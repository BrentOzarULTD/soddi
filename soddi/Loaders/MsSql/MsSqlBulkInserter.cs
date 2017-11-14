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
using System.Data.SqlClient;

#endregion

namespace Salient.StackExchange.Import.Loaders.MsSql
{
    public class MsSqlBulkCopy : BulkCopyBase
    {
        private readonly SqlBulkCopy _inner;
        private readonly string _connectionString;
        private readonly bool _identity;

        public MsSqlBulkCopy(string connectionString, SqlBulkCopyOptions options)
        {
            _connectionString = connectionString;
            _identity = options.HasFlag(SqlBulkCopyOptions.KeepIdentity);
            _inner = new SqlBulkCopy(connectionString, options);
            _inner.SqlRowsCopied += (s, e) =>
                {
                    BulkCopyEventArgs args = new BulkCopyEventArgs {Count = e.RowsCopied, Type = CopyEventType.Active};
                    OnRowsInserted(args);

                    e.Abort = args.Abort;
                };
        }


        public override int BatchSize
        {
            get { return _inner.BatchSize; }
            set { _inner.BatchSize = value; }
        }

        public override int BulkCopyTimeout
        {
            get { return _inner.BulkCopyTimeout; }
            set { _inner.BulkCopyTimeout = value; }
        }

        public override string DestinationTableName
        {
            get { return _inner.DestinationTableName; }
            set { _inner.DestinationTableName = value; }
        }

        public override int NotifyAfter
        {
            get { return _inner.NotifyAfter; }
            set { _inner.NotifyAfter = value; }
        }


        public override void AddColumnMappings(string source, string target, DbType ignored)
        {
            _inner.ColumnMappings.Add(source, target);
        }


        public override void WriteToServer(IDataReader reader)
        {
            if (_identity)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"SET IDENTITY_INSERT {DestinationTableName} ON";
                        command.ExecuteNonQuery();
                    }
                }
            }
            try
            {
                _inner.WriteToServer(reader);
            }
            finally
            {
                if (_identity)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.Text;
                            command.CommandText = $"SET IDENTITY_INSERT {DestinationTableName} OFF";
                            command.ExecuteNonQuery();
                        }

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.Text;
                            command.CommandText =
                                $"DBCC CHECKIDENT('{DestinationTableName.Replace("[", string.Empty).Replace("]", string.Empty)}', RESEED)";
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}