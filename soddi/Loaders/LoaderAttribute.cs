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
    [AttributeUsage(AttributeTargets.Class)]
    public class LoaderAttribute : Attribute
    {
        public LoaderAttribute(string providerInvariantName)
        {
            ProviderInvariantName = providerInvariantName;
        }

        public string ProviderInvariantName { get; set; }
    }
}