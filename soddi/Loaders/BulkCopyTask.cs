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
using Salient.Data;
using Salient.StackExchange.Import.Tools;

#endregion

namespace Salient.StackExchange.Import.Loaders
{
    /// <summary>
    /// Streams an EnumerableDataReader into the destination table via SqlBulkCopy
    /// </summary>
    public class BulkCopyTask
    {
        public event EventHandler Complete;
        public event EventHandler PostProcess;

        public virtual void OnPostProcess()
        {
            OnRowsInserted(CopyEventType.Processing);
            if (PostProcess != null)
                PostProcess(this, EventArgs.Empty);
        }

        public virtual void OnComplete()
        {
            OnRowsInserted(CopyEventType.Complete);
            if (Complete != null)
                Complete(this, EventArgs.Empty);
        }

        #region Fields

        private readonly int _batchSize;

        private readonly BulkCopyBase _bc;

        private readonly Guid _id;

        private readonly EnumerableDataReader _readerIn;

        private readonly string _schema;

        private readonly string _site;

        private readonly string _table;
        private long _count;
        private CopyEventType _state;

        #endregion

        #region Events

        public event EventHandler<BulkCopyEventArgs> RowsInserted;

        #endregion

        #region Constructors

        internal BulkCopyTask(BulkCopyBase bc, string table, EnumerableDataReader readerIn, string site, int batchSize,
                              string schema)
        {
            _id = Guid.NewGuid();
            _bc = bc;
            _site = site;
            _table = table;
            _schema = schema;
            _batchSize = batchSize;
            _readerIn = readerIn;
        }

        #endregion

        #region Properties

        public long Count
        {
            get { return _count; }
        }

        public Guid Id
        {
            get { return _id; }
        }

        public string Schema
        {
            get { return _schema; }
        }

        public string Site
        {
            get { return _site; }
        }

        public CopyEventType State
        {
            get { return _state; }
        }

        public string Table
        {
            get { return _table; }
        }

        public string Tag { get; set; }

        #endregion

        #region Public Methods

        public void Process()
        {
            _count = 0;
            bool aborted = false;
            try
            {
                OnRowsInserted(CopyEventType.Begin, "Initializing");

                if (_batchSize > 0)
                {
                    _bc.BatchSize = _batchSize;
                    _bc.NotifyAfter = _batchSize;
                }

                _bc.RowsInserted += (s, e) =>
                {
                        _count = e.Count;
                        e.Abort = OnRowsInserted(CopyEventType.Active);
                        if (e.Abort)
                        {
                            OnRowsInserted(CopyEventType.Error, "Aborted");
                            aborted = true;
                        }
                    };
                _bc.BulkCopyTimeout = 35000;

                _bc.DestinationTableName = string.IsNullOrEmpty(Schema)
                                               ? Table
                                               : string.Format("[{0}].{1}", Schema, Table);

                DataTable st = _readerIn.GetSchemaTable();

                using (EnumerableDataReader reader = _readerIn)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string fieldName = reader.GetName(i);

                        _bc.AddColumnMappings(fieldName, fieldName, TypeConverter.ToDbType(st.Columns[i].DataType));
                    }

                    _bc.WriteToServer(reader);

                    reader.Close();

                    if (!aborted)
                    {
                        OnPostProcess();
                    }
                    OnComplete();
                }
            }
            catch (Exception ex)
            {
                OnRowsInserted(CopyEventType.Error, ex.Message);
            }
        }

        #endregion

        #region Protected Methods

        public virtual bool OnRowsInserted(CopyEventType type, string message)
        {
            _state = type;
            BulkCopyEventArgs ea = new BulkCopyEventArgs(type, Id, message, _count);
            if (RowsInserted != null)
                RowsInserted(null, ea);
            return ea.Abort;
        }

        protected virtual bool OnRowsInserted(CopyEventType type)
        {
            return OnRowsInserted(type, string.Empty);
        }

        #endregion
    }
}