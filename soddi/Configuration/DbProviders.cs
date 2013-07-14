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
// 04/09/2010 - added a catch to prevent crash if provider not found


#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Salient.StackExchange.Import.Loaders;

#endregion

namespace Salient.StackExchange.Import.Configuration
{
    public sealed class DbProviders : List<DbProviderInfo>
    {
        private DbProviders()
        {
            List<Type> insertTypes =
                Assembly.GetExecutingAssembly().GetTypes().Where(
                    t =>
                    t.BaseType == typeof (BulkLoader) &&
                    t.GetCustomAttributes(typeof (LoaderAttribute), true).Length > 0).ToList();
            foreach (DataRow prov in DbProviderFactories.GetFactoryClasses().Rows)
            {
                string invariant = (string) prov["InvariantName"];
                try
                {
                    Type type =
                insertTypes.Find(
                    t =>
                    ((LoaderAttribute)t.GetCustomAttributes(typeof(LoaderAttribute), true)[0]).
                        ProviderInvariantName == invariant);
                    if (type != null)
                    {
                        Add(new DbProviderInfo(prov) { BulkInsertType = type });
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error adding provider {0}:\r\n{1}",invariant,ex.Message);
                }
            }
        }

        public static DbProviders Instance
        {
            get { return Nested.Instance; }
        }

        public static DbProviderInfo ParseArg(string target)
        {
            // check to see if target is a named connection string
            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[target];

            if (css != null)
            {
                DbProviderInfo provider =
                    Instance.Find(p => string.Compare(css.ProviderName, p.InvariantName, true) == 0);
                provider.ConnectionString = css.ConnectionString;
                return provider;
            }

            // check to see if a valid connection string
            DbConnectionStringBuilder csb = new DbConnectionStringBuilder {ConnectionString = target};
            string providerName = csb["Provider"] as string;

            if (!string.IsNullOrEmpty(providerName))
            {
                DbProviderInfo provider = Instance.Find(p => string.Compare(providerName, p.InvariantName, true) == 0);
                csb.Remove("provider");
                provider.ConnectionString = csb.ConnectionString;
                return provider;
            }
            return null;
        }

        #region Nested type: Nested

        private class Nested
        {
            internal static readonly DbProviders Instance = new DbProviders();
        }

        #endregion
    }
}