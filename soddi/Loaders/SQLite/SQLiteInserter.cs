// /*!
//  * Project: SODDI v.10
//  * http://skysanders.net/tools/se/
//  *
//  * Copyright 2010, Sky Sanders
//  * Dual licensed under the MIT or GPL Version 2 licenses.
//  * http://skysanders.net/tools/se/LICENSE.TXT
//  *
//  * Date: April 01 2010 
//  */

#region

using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Salient.Data;
using Salient.StackExchange.Import.Tools;

#endregion

namespace Salient.StackExchange.Import.Loaders.SQLite
{
    [Loader("System.Data.SQLite")]
    public class SQLiteInserter : BulkLoader
    {
        /// <summary>
        /// An overly aggressive attempt at maximizing performance 
        /// </summary>
        private const string Pragma =
            "PRAGMA synchronous=OFF;PRAGMA count_changes = FALSE;PRAGMA journal_mode = OFF;PRAGMA locking_mode = NORMAL;PRAGMA page_size = 32768;PRAGMA temp_store = MEMORY;PRAGMA foreign_keys=OFF";

        /// <summary>
        /// One lock for each site's db3 file. For use in controlling merge
        /// </summary>
        private readonly Hashtable _locks = new Hashtable();

        private string _targetPath;

        public SQLiteInserter(Configuration.Configuration config)
            : base(config)
        {
        }


        public override void Configure()
        {
            SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder(Config.Provider.ConnectionString);
            _targetPath = csb.DataSource;


            foreach (ImportTarget target in Config.Targets)
            {
                PrepareOutputDataFile(_targetPath, target.Schema);
                _locks[target.Name] = new object();
                Jobs.Add(new BulkLoadJob {Tag = target.Schema});
            }

            base.Configure();
        }


        public override void CreateBulkInsertTask(string table, EnumerableDataReader reader, ImportTarget target)
        {
            BulkCopyTask task = new BulkCopyTask(new SQLiteBulkInserter(_targetPath, target.Schema, table), table,
                                                 reader, target.Name, Config.BatchSize, target.Schema);
            task.PostProcess += PostProcess; // when the task is complete, merge into main file
            _locks[task.Schema] = new object();
            Jobs.Find(j => j.Tag == target.Schema).Tasks.Add(task);
        }


        private void CreateMainDbFile(string connectionString)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Pragma;
                    cmd.ExecuteNonQuery();

                    string script = Common.GetTextResource("Loaders\\Scripts\\sqlite-main.sql",
                                                           Assembly.GetExecutingAssembly());
                    if (Config.Indices)
                    {
                        script = script.Replace("-- INDICES", "");
                    }
                    if (Config.Identity)
                    {
                        script = script.Replace("/* IDENTITY */", "AUTOINCREMENT");
                    }
                    cmd.CommandText = script;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static string GetConnectionString(string targetPath, string schema)
        {
            return string.Format(@"Data Source={0};Version=3;New=True;", GetOutputPath(targetPath, schema));
        }

        private static string GetOutputPath(string targetPath, string schema)
        {
            return Path.Combine(targetPath, string.Format("{0}.db3", schema));
        }

        private static void PrepareOutputDatabaseFile(string targetPath, string schema)
        {
            Directory.CreateDirectory(targetPath);
            string path = GetOutputPath(targetPath, schema);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
        }

        private void PrepareOutputDataFile(string targetPath, string schema)
        {
            PrepareOutputDatabaseFile(targetPath, schema);
            CreateMainDbFile(GetConnectionString(targetPath, schema));
        }

        private void PostProcess(object sender, EventArgs e)
        {
            const string transferFormat = "INSERT INTO main.{0} ({1} ) SELECT {1} from tmp{0}.{0}_tmp;";
            BulkCopyTask task = (BulkCopyTask) sender;
            if (task.State != CopyEventType.Processing)
            {
                // dont want to merge suspect results
                return;
            }

            task.OnRowsInserted(CopyEventType.Processing, "Waiting to merge...");

            string table = task.Table;

            string sourcePath = Path.Combine(_targetPath, string.Format("{0}.{1}.db3", task.Schema, table));
            lock (_locks[task.Schema])
            {
                task.OnRowsInserted(CopyEventType.Processing, "Merging...");

                using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString(_targetPath, task.Schema)))
                {
                    conn.Open();
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = Pragma;
                    cmd.ExecuteNonQuery();
                    try
                    {
                        cmd.CommandText = string.Format("ATTACH DATABASE \"{1}\" as [tmp{0}]", table, sourcePath);
                        cmd.ExecuteNonQuery();
                        using (SQLiteTransaction tx = conn.BeginTransaction())
                        {
                            DataTable tableInfo = conn.GetSchema("Columns", new[] {"tmp" + table, null, table + "_tmp"});
                            string columnList = string.Join(",",
                                                            tableInfo.Select().Select(r => r["COLUMN_NAME"]).Cast
                                                                <string>().
                                                                ToArray());
                            cmd.CommandText = string.Format(transferFormat, table, columnList);
                            cmd.ExecuteNonQuery();
                            tx.Commit();
                        }

                        cmd.CommandText = string.Format("DETACH DATABASE [tmp{0}]", table);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        task.OnRowsInserted(CopyEventType.Error, ex.Message);
                        return;
                    }

                    Thread.Sleep(100);
                    File.Delete(sourcePath);
                }
            }
        }
    }
}