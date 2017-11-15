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
using System.Data.Common;

#endregion

namespace Salient.StackExchange.Import.Configuration
{
    public class DbProviderInfo
    {
        public DbProviderInfo(DataRow prov)
        {
            Name = (string) prov["Name"];
            Description = (string) prov["Description"];
            InvariantName = (string) prov["InvariantName"];
            AssemblyQualifiedName = (string) prov["AssemblyQualifiedName"];

            Factory = DbProviderFactories.GetFactory(prov);
        }

        public DbProviderInfo(string name, string description, string invariantName, string assemblyQualifiedName,
            DbProviderFactory factory)
        {
            Name = name;
            Description = description;
            InvariantName = invariantName;
            AssemblyQualifiedName = assemblyQualifiedName;
            Factory = factory;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string InvariantName { get; set; }
        public string AssemblyQualifiedName { get; set; }
        public Type BulkInsertType { get; set; }
        public DbProviderFactory Factory { get; private set; }
        public String ConnectionString { get; set; }
    }
}