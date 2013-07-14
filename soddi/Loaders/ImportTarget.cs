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
    public class ImportTarget
    {
        #region Fields

        public String Name;
        public string Path;

        public string Schema;

        #endregion

        #region Constructors

        public ImportTarget(string site, string path, string schema)
        {
            Name = site;
            Path = path;
            Schema = schema;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}