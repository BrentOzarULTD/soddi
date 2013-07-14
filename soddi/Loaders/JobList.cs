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
using System.Collections.Generic;
using Salient.StackExchange.Import.Loaders;

#endregion

namespace Salient.StackExchange.Import
{
    public class JobList : List<BulkLoadJob>
    {
        #region Fields

        private CopyEventType _state;

        #endregion

        #region Events

        public event EventHandler Complete;

        public event EventHandler<BulkCopyEventArgs> RowsInserted;

        #endregion

        #region Properties

        public CopyEventType State
        {
            get { return _state; }
        }

        #endregion

        #region Public Methods

        public new void Add(BulkLoadJob job)
        {
            job.RowsInserted += (s, e) => OnRowsInserted(e);
            base.Add(job);
        }

        public BulkCopyTask FindTask(Guid id)
        {
            BulkCopyTask task = null;
            foreach (BulkLoadJob job in this)
            {
                task = job.Tasks.Find(t => t.Id == id);
                if (task != null)
                {
                    break;
                }
            }
            return task;
        }

        #endregion

        #region Protected Methods

        protected virtual void OnComplete()
        {
            if (Complete != null)
                Complete(this, EventArgs.Empty);
        }

        protected virtual bool OnRowsInserted(BulkCopyEventArgs e)
        {
            bool hasErrors = false;
            bool isComplete = true;
            foreach (BulkLoadJob job in this)
            {
                if (job.Tasks.State == CopyEventType.Error)
                {
                    hasErrors = true;
                }
                if (!(job.Tasks.State == CopyEventType.Error || job.Tasks.State == CopyEventType.Complete))
                {
                    isComplete = false;
                }
            }

            _state = isComplete ? hasErrors ? CopyEventType.Error : CopyEventType.Complete : CopyEventType.Active;

            if (RowsInserted != null)
                RowsInserted(null, e);


            if (_state == CopyEventType.Complete || _state == CopyEventType.Error)
            {
                OnComplete();
            }

            return e.Abort;
        }

        protected virtual bool OnRowsInserted(object sender, BulkCopyEventArgs e)
        {
            return OnRowsInserted(e);
        }

        #endregion
    }
}