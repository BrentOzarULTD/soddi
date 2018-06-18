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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Salient.Reflection;
using Salient.StackExchange.Import.TableTypes;

#endregion

namespace Salient.StackExchange.Import.Loaders
{
    /// <summary>
    /// Provides fast deserialization functionality via dynamic
    /// getter/setters.
    /// 
    /// The properties of the derived classes correspond directly
    /// to both the source xml schema and the destination schema.
    /// 
    /// Any changes to these object will not result in rainbows and
    /// unicorns. You are warned.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SoBase<T> where T : class, ISoBase, new()
    {
        #region Readonly

        private static readonly IList<DynamicProperties.Property> Props;

        #endregion

        #region Constructors

        static SoBase()
        {
            Props = DynamicProperties.CreatePropertyMethods(typeof (T));
        }

        #endregion

        #region Public Methods

        public static T FromXElement(XElement r)
        {
            T result = new T();
            foreach (DynamicProperties.Property prop in Props)
            {
                XAttribute attribute = r.Attribute(prop.Info.Name);
                if (attribute != null)
                {
                    prop.Setter(result, GetValueOrDefault(attribute, prop.Info.PropertyType));
                }
            }

            return result;
        }

        /// <summary>
        /// Types and returns the value of an attribute or the default of the
        /// type if the attribute is null.
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        private static object GetValueOrDefault(XAttribute attr, Type returnType)
        {
            if (attr == null)
            {
                return returnType.IsValueType ? Activator.CreateInstance(returnType) : null;
            }

            Type baseType = returnType;

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                baseType = returnType.GetGenericArguments()[0];
            }

            return TypeDescriptor.GetConverter(baseType).ConvertFromInvariantString(attr.Value);
        }

        public static string GetTableName(Type t)
        {
            return (string) t.GetMethod("GetFileName").Invoke(null, null);
        }

        /// <summary>
        /// Returns a streaming sequence of T deserialized from a stackoverflow
        /// data dump xml document.
        /// </summary>
        /// <typeparam name="T">The type of domain object to stream</typeparam>
        /// <param name="path">A stackoverflow data dump directory</param>
        /// <param name="site"></param>
        /// <returns></returns>
        public static IEnumerable<T> FromXmlDocument(string path, string site)
        {
            Regex rx = new Regex(@"\<([^>]+)\>", RegexOptions.Compiled);

            string filename = GetTableName(typeof (T));
            string filePath = Path.Combine(path, filename);

            using (XmlReader rdr = XmlReader.Create(filePath))
            {
                rdr.MoveToContent();
                while (rdr.Read())
                {
                    if ((rdr.NodeType == XmlNodeType.Element) && (rdr.Name == "row"))
                    {
                        XElement node = (XElement) XNode.ReadFrom(rdr);
                        if (typeof (T) == typeof (PostTags))
                        {
                            XAttribute tagsAtt = node.Attributes("Tags").FirstOrDefault();
                            if (tagsAtt != null)
                            {
                                // ReSharper disable PossibleNullReferenceException
                                int postId = Convert.ToInt32(node.Attribute("Id").Value);
                                // ReSharper restore PossibleNullReferenceException
                                IEnumerable<string> distinctTags =
                                    rx.Matches(tagsAtt.Value).Cast<Match>().Select(m => m.Groups[1].Value).Distinct();
                                foreach (string tag in distinctTags)
                                {
                                    T newPostTags = new PostTags {Tag = tag, PostId = postId} as T;
                                    yield return newPostTags;
                                }
                            }
                        }
                        else
                        {
                            yield return FromXElement(node);
                        }
                    }
                }
                rdr.Close();
            }
        }

        #endregion
    }
}