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
using System.Reflection;
using MySql.Data.MySqlClient;
using Salient.Data;
using Salient.StackExchange.Import.Tools;

#endregion

namespace Salient.StackExchange.Import.Loaders.MySql
{
    [Loader("MySql.Data.MySqlClient")]
    public class MySqlInserter : BulkLoader
    {
        public MySqlInserter(Configuration.Configuration config)
            : base(config)
        {
        }

        public override void Configure()
        {
            Type[] allTableTypes = Config.GetAllTableTypes();
            foreach (Type item in allTableTypes)
            {
                Jobs.Add(new BulkLoadJob {Tag = item.Name});
            }

            base.Configure();
        }

        public override void CreateBulkInsertTask(string table, EnumerableDataReader reader, ImportTarget target)
        {
            BulkCopyTask task =
                new BulkCopyTask(new MySqlBulkInserter(Config.Provider.ConnectionString, table, target.Schema), table,
                                 reader, target.Name, Config.BatchSize, target.Schema);

            Jobs.Find(j => j.Tag == table).Tasks.Add(task);
        }


        public override void PrepareDatabase(string schema)
        {
            using (MySqlConnection conn = new MySqlConnection(Config.Provider.ConnectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 300;

                    string script = Common.GetTextResource("Loaders\\Scripts\\mysql.sql",
                                                           Assembly.GetExecutingAssembly());

                    script = script.Replace("DUMMY", "`" + schema + "`");

                    if ((Config.Options & Configuration.Configuration.ImportOptions.Split) ==
                        Configuration.Configuration.ImportOptions.Split)
                    {
                        script = script.Replace("IF 0 = 1--SPLIT", "");
                    }
                    if ((Config.Options & Configuration.Configuration.ImportOptions.FullText) ==
                        Configuration.Configuration.ImportOptions.FullText)
                    {
                        script = script.Replace("IF 0 = 1--FULLTEXT", "");
                    }
                    if ((Config.Options & Configuration.Configuration.ImportOptions.Indices) ==
                        Configuration.Configuration.ImportOptions.Indices)
                    {
                        script = script.Replace("IF 0 = 1-- INDICES", "");
                    }
                    if ((Config.Options & Configuration.Configuration.ImportOptions.Indices) ==
                        Configuration.Configuration.ImportOptions.Indices)
                    {
                        script = script.Replace("-- INDICES", "");
                    }
                    if ((Config.Options & Configuration.Configuration.ImportOptions.Identity) ==
                        Configuration.Configuration.ImportOptions.Identity)
                    {
                        script = script.Replace("/* IDENTITY */", "AUTO_INCREMENT");
                    }
                    cmd.CommandText = script;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}