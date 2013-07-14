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
using System.Threading;

#endregion

namespace Salient.StackExchange.Import.Loaders
{
    /// <summary>
    /// Manages a group of related BulkInsertTasks that are to
    /// be run sequentially. In this case, we are cuing all updates
    /// to a particular table into one BulkInsertJob to prevent deadlocks.
    /// </summary>
    public class BulkLoadJob
    {
        #region Fields

        private readonly TaskList _tasks;

        #endregion

        #region Events

        public event EventHandler Complete;

        public event EventHandler<BulkCopyEventArgs> RowsInserted;

        #endregion

        #region Constructors

        public BulkLoadJob()
        {
            _tasks = new TaskList();
            _tasks.Complete += (s, e) => OnComplete();
            _tasks.RowsInserted += (s, e) => OnRowsInserted(e);
        }

        #endregion

        #region Properties

        public string Tag { get; set; }

        public TaskList Tasks
        {
            get { return _tasks; }
        }

        #endregion

        #region Public Methods

        public BulkCopyTask Find(string site)
        {
            return _tasks.Find(t => t.Site == site);
        }

        public virtual void OnComplete()
        {
            if (Complete != null)
                Complete(this, EventArgs.Empty);
        }

        public void Process()
        {
            List<Thread> threads = new List<Thread>();
            foreach (BulkCopyTask task in _tasks)
            {
#if NOTHREAD
                task.Process();
#else
                Thread t = new Thread(task.Process);
                threads.Add(t);
                t.Start();
#endif
            }
            threads.ForEach(t => t.Join());
        }

        #endregion

        #region Protected Methods

        protected virtual bool OnRowsInserted(object sender, BulkCopyEventArgs e)
        {
            return OnRowsInserted(e);
        }

        protected virtual bool OnRowsInserted(BulkCopyEventArgs e)
        {
            if (RowsInserted != null)
                RowsInserted(null, e);
            return e.Abort;
        }

        #endregion
    }
}