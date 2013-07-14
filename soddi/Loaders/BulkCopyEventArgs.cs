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

#endregion

namespace Salient.StackExchange.Import.Loaders
{
    public class BulkCopyEventArgs : EventArgs
    {
        #region Fields

        #endregion

        #region Constructors

        public BulkCopyEventArgs()
        {
        }

        public BulkCopyEventArgs(CopyEventType type, Guid id, string message, long count)
        {
            Count = count;
            Type = type;
            Message = message;
            Id = id;
        }

        #endregion

        #region Properties

        public bool Abort { get; set; }

        public long Count { get; set; }

        public Guid Id { get; set; }

        public string Message { get; set; }

        public CopyEventType Type { get; set; }

        #endregion
    }
}