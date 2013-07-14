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
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Salient.StackExchange.Import.Tools;

#endregion

namespace Salient.StackExchange.Import.Loaders.SQLite
{
    public class SQLiteBulkInserter : BulkCopyBase
    {
        private readonly SQLiteCommand _command;

        private readonly string _schema;

        private readonly string _source;

        private readonly string _table;


        public SQLiteBulkInserter(string source, string schema, string table)
        {
            _table = table;

            _command = new SQLiteCommand();
            _schema = schema;
            _source = source;
        }


        public override void AddColumnMappings(string source, string target, DbType type)
        {
            _command.Parameters.Add(new SQLiteParameter(target, type, source));
        }

        public override void WriteToServer(IDataReader reader)
        {
            const string insertFormat = "insert into {0}_tmp ({1}) values ({2});";

            string path = Path.Combine(_source, _schema + "." + _table + ".db3");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string connectionString = string.Format("Data Source={0};Version=3;New=True;", path);


            int reportCounter = 0;
            int totalRecords = 0;
            bool finished = false;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                _command.Connection = connection;
                _command.CommandText = "pragma journal_mode = off;pragma foreign_keys=off;";
                _command.ExecuteNonQuery();
                _command.CommandText =
                    Common.GetTextResource("Loaders\\Scripts\\sqlite-tmp.sql", Assembly.GetExecutingAssembly());
                _command.ExecuteNonQuery();

                string names = string.Join(",",
                                           _command.Parameters.Cast<SQLiteParameter>().Select(p => p.ParameterName).
                                               ToArray());
                string vals = string.Join(",", _command.Parameters.Cast<object>().Select(p => "?").ToArray());
                _command.CommandText = string.Format(insertFormat, _table, names, vals);


                _command.Connection = connection;

                while (!finished)
                {
                    using (DbTransaction dbTrans = connection.BeginTransaction())
                    {
                        for (int i = 0; i < BatchSize; i++)
                        {
                            if (!reader.Read())
                            {
                                finished = true;
                                break;
                            }

                            try
                            {
                                for (int p = 0; p < _command.Parameters.Count; p++)
                                {
                                    _command.Parameters[p].Value = reader.GetValue(p);
                                }
                                _command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine(ex.Message);
                                throw;
                            }
                            reportCounter++;
                            totalRecords++;

                            if (reportCounter >= NotifyAfter)
                            {
                                reportCounter = 0;
                                BulkCopyEventArgs args = new BulkCopyEventArgs
                                    {Count = totalRecords, Type = CopyEventType.Active};
                                OnRowsInserted(args);
                                if (args.Abort)
                                {
                                    finished = true;
                                    break;
                                }
                            }
                        }
                        dbTrans.Commit();
                    }
                }
            }
        }
    }
}