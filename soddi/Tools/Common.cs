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
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace Salient.StackExchange.Import.Tools
{
    public static class Common
    {
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke(action);
        }

        public static string GetTextResource(string resourcePath, Assembly assembly)
        {
            if (File.Exists(resourcePath))
            {
                return File.ReadAllText(resourcePath);
            }

            resourcePath = typeof (Program).Namespace + "." + resourcePath.Replace("\\", ".");
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream != null)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string script = reader.ReadToEnd();
                        return script;
                    }
            }
            throw new FileNotFoundException(resourcePath);
        }
    }

    /// <summary>
    /// Convert a base data type to another base data type
    /// </summary>
    public static class TypeConverter
    {
        #region Readonly

        private static readonly List<DbTypeMapEntry> DbTypeList = new List<DbTypeMapEntry>();

        #endregion

        #region Structs

        private struct DbTypeMapEntry
        {
            public readonly DbType DbType;
            public readonly SqlDbType SqlDbType;
            public readonly Type Type;


            public DbTypeMapEntry(Type type, DbType dbType, SqlDbType sqlDbType)
            {
                Type = type;
                DbType = dbType;
                SqlDbType = sqlDbType;
            }
        } ;

        #endregion

        #region Constructors

        static TypeConverter()
        {
            DbTypeMapEntry dbTypeMapEntry = new DbTypeMapEntry(typeof (bool), DbType.Boolean, SqlDbType.Bit);
            DbTypeList.Add(dbTypeMapEntry);
            dbTypeMapEntry = new DbTypeMapEntry(typeof (byte), DbType.Double, SqlDbType.TinyInt);
            DbTypeList.Add(dbTypeMapEntry);
            dbTypeMapEntry = new DbTypeMapEntry(typeof (byte[]), DbType.Binary, SqlDbType.Image);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (DateTime), DbType.DateTime, SqlDbType.DateTime);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (Decimal), DbType.Decimal, SqlDbType.Decimal);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (double), DbType.Double, SqlDbType.Float);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (Guid), DbType.Guid, SqlDbType.UniqueIdentifier);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (Int16), DbType.Int16, SqlDbType.SmallInt);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (Int32), DbType.Int32, SqlDbType.Int);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (Int64), DbType.Int64, SqlDbType.BigInt);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (object), DbType.Object, SqlDbType.Variant);
            DbTypeList.Add(dbTypeMapEntry);

            dbTypeMapEntry = new DbTypeMapEntry(typeof (string), DbType.String, SqlDbType.VarChar);
            DbTypeList.Add(dbTypeMapEntry);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Convert TSQL data type to DbType
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static DbType ToDbType(SqlDbType sqlDbType)
        {
            DbTypeMapEntry entry = Find(sqlDbType);
            return entry.DbType;
        }

        /// <summary>
        /// Convert .Net type to Db type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbType ToDbType(Type type)
        {
            DbTypeMapEntry entry = Find(type);
            return entry.DbType;
        }

        /// <summary>
        /// Convert TSQL type to .Net data type
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static Type ToNetType(SqlDbType sqlDbType)
        {
            DbTypeMapEntry entry = Find(sqlDbType);
            return entry.Type;
        }

        /// <summary>
        /// Convert db type to .Net data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static Type ToNetType(DbType dbType)
        {
            DbTypeMapEntry entry = Find(dbType);
            return entry.Type;
        }

        /// <summary>
        /// Convert DbType type to TSQL data type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlDbType ToSqlDbType(DbType dbType)
        {
            DbTypeMapEntry entry = Find(dbType);
            return entry.SqlDbType;
        }

        /// <summary>
        /// Convert .Net type to TSQL data type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqlDbType ToSqlDbType(Type type)
        {
            DbTypeMapEntry entry = Find(type);
            return entry.SqlDbType;
        }

        #endregion

        #region Private Methods

        private static DbTypeMapEntry Find(Type type)
        {
            object retObj = null;
            for (int i = 0; i < DbTypeList.Count; i++)
            {
                DbTypeMapEntry entry = DbTypeList[i];
                if (entry.Type == type)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
            {
                throw new ApplicationException("Referenced an unsupported Type");
            }
            return (DbTypeMapEntry) retObj;
        }

        private static DbTypeMapEntry Find(DbType dbType)
        {
            object retObj = null;
            for (int i = 0; i < DbTypeList.Count; i++)
            {
                DbTypeMapEntry entry = DbTypeList[i];
                if (entry.DbType == dbType)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
            {
                throw new ApplicationException("Referenced an unsupported DbType");
            }
            return (DbTypeMapEntry) retObj;
        }

        private static DbTypeMapEntry Find(SqlDbType sqlDbType)
        {
            object retObj = null;
            for (int i = 0; i < DbTypeList.Count; i++)
            {
                DbTypeMapEntry entry = DbTypeList[i];
                if (entry.SqlDbType == sqlDbType)
                {
                    retObj = entry;
                    break;
                }
            }
            if (retObj == null)
            {
                throw new ApplicationException("Referenced an unsupported SqlDbType");
            }

            return (DbTypeMapEntry) retObj;
        }

        #endregion
    }
}