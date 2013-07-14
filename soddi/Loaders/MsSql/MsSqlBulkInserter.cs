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

using System.Data;
using System.Data.SqlClient;

#endregion

namespace Salient.StackExchange.Import.Loaders.MsSql
{
    public class MsSqlBulkCopy : BulkCopyBase
    {
        private readonly SqlBulkCopy _inner;


        public MsSqlBulkCopy(string connectionString, SqlBulkCopyOptions options)
        {
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
            _inner.WriteToServer(reader);
        }
    }
}