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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Salient.Data;

#endregion

namespace Salient.StackExchange.Import.Loaders
{
    public abstract class BulkLoader
    {
        #region Fields

        private readonly JobList _jobs;

        protected Configuration.Configuration Config;

        #endregion

        #region Events

        public event EventHandler<BulkCopyEventArgs> RowsInserted;

        #endregion

        #region Constructors

        protected BulkLoader(Configuration.Configuration config)
        {
            _jobs = new JobList();
            _jobs.RowsInserted += (s, e) => OnRowsInserted(e);
            Config = config;
        }

        #endregion

        #region Properties

        public JobList Jobs
        {
            get { return _jobs; }
        }

        #endregion

        #region Public Methods

        public virtual void Configure()
        {
            foreach (ImportTarget target in Config.Targets)
            {
                foreach (Type type in Config.GetAllTableTypes())
                {
                    MethodInfo methodInfo = typeof (SoBase<>).MakeGenericType(type).GetMethod("FromXmlDocument");
                    IEnumerable sequence =
                        (IEnumerable) methodInfo.Invoke(null, new object[] {target.Path, target.Name});
                    EnumerableDataReader reader = new EnumerableDataReader(sequence);

                    CreateBulkInsertTask(type.Name, reader, target);
                }

                PrepareDatabase(target.Schema);
            }
        }

        public abstract void CreateBulkInsertTask(string table, EnumerableDataReader reader, ImportTarget target);

        public static BulkLoader Create(Configuration.Configuration config)
        {
            Type providerType = config.Provider.BulkInsertType;
            BulkLoader result = (BulkLoader) Activator.CreateInstance(providerType, new object[] {config});
            result.Configure();
            return result;
        }


        public virtual void PrepareDatabase(string schema)
        {
            /*noop*/
        }

        public virtual void ProcessJobs(Configuration.Configuration config)
        {
            List<Thread> threads = new List<Thread>();

            foreach (BulkLoadJob job in Jobs)
            {
#if NOTHREAD
                job.Process();
#else
                Thread t = new Thread(job.Process);
                threads.Add(t);
                t.Start();
#endif
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
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
                RowsInserted(this, e);
            return e.Abort;
        }

        #endregion
    }
}