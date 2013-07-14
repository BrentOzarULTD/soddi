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
using System.Data.SqlClient;
using System.Reflection;
using Salient.Data;
using Salient.StackExchange.Import.Tools;

#endregion

namespace Salient.StackExchange.Import.Loaders.MsSql
{
    [Loader("System.Data.SqlClient")]
    public class MSSqlInserter : BulkLoader
    {
        public MSSqlInserter(Configuration.Configuration config)
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
                new BulkCopyTask(new MsSqlBulkCopy(Config.Provider.ConnectionString, SqlBulkCopyOptions.TableLock),
                                 table, reader, target.Name, Config.BatchSize, target.Schema);

            Jobs.Find(j => j.Tag == table).Tasks.Add(task);
        }

        public override void PrepareDatabase(string schema)
        {
            using (SqlConnection conn = new SqlConnection(Config.Provider.ConnectionString))
            {
                conn.Open();


                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = string.Format("CREATE SCHEMA {0} AUTHORIZATION [dbo]", schema);

                    try
                    {
                        // have to wrap it up or delete it first. would rather fail on exists
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }


                    string script = Common.GetTextResource("Loaders\\Scripts\\mssql.sql",
                                                           Assembly.GetExecutingAssembly());
                    script = script.Replace("PostFullText",
                                            string.Format("{0}PostFullText", schema.Replace(" ", "").Trim('[', ']')));
                    script = script.Replace("DUMMY", schema);


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
                    cmd.CommandText = script;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}