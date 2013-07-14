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

#endregion

namespace Salient.StackExchange.Import.Loaders
{
    public abstract class BulkCopyBase
    {
        public virtual int BatchSize { get; set; }
        public virtual int BulkCopyTimeout { get; set; }
        public virtual string DestinationTableName { get; set; }
        public virtual int NotifyAfter { get; set; }
        public event EventHandler<BulkCopyEventArgs> RowsInserted;

        protected virtual void OnRowsInserted(BulkCopyEventArgs ea)
        {
            if (RowsInserted != null)
                RowsInserted(this, ea);
        }

        public abstract void AddColumnMappings(string source, string target, DbType type);

        public virtual void WriteToServer(IDataReader reader)
        {
        }
    }
}