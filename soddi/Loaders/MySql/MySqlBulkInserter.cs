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
using System.Diagnostics;
using System.Linq;
using MySql.Data.MySqlClient;

#endregion

namespace Salient.StackExchange.Import.Loaders.MySql
{
    public class MySqlBulkInserter : BulkCopyBase
    {
        private readonly MySqlCommand _command;

        private readonly string _schema;

        private readonly string _source;

        private readonly string _table;


        public MySqlBulkInserter(string source, string table, string schema)
        {
            _schema = schema;
            _table = table;

            _command = new MySqlCommand();
            _source = source;
        }


        public override void AddColumnMappings(string source, string target, DbType type)
        {
            _command.Parameters.Add(new MySqlParameter(target, null) {SourceColumn = source});
        }

        public override void WriteToServer(IDataReader reader)
        {
            const string insertFormat = "insert into `{3}`.`{0}` ({1}) values ({2});";

            string names = string.Join(",",
                                       _command.Parameters.Cast<MySqlParameter>().Select(p => p.ParameterName).ToArray());
            string vals = string.Join(",",
                                      _command.Parameters.Cast<MySqlParameter>().Select(p => "?" + p.ParameterName).
                                          ToArray());

            _command.CommandText = string.Format(insertFormat, _table, names, vals, _schema);

            int reportCounter = 0;
            int totalRecords = 0;
            bool finished = false;

            using (MySqlConnection connection = new MySqlConnection(_source))
            {
                connection.Open();
                _command.Connection = connection;
                _command.Prepare();

                while (!finished)
                {
                    using (MySqlTransaction dbTrans = connection.BeginTransaction(IsolationLevel.ReadUncommitted))
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