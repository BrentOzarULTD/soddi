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
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;
using Salient.StackExchange.Import.Loaders;
using Salient.StackExchange.Import.TableTypes;

#endregion

namespace Salient.StackExchange.Import.Configuration
{
    public class Configuration
    {
        #region ImportOptions enum

        [Flags]
        public enum ImportOptions
        {
            None = 0,
            Indices = 1,
            FullText = 2,
            Split = 4,
            GUI = 8,
            FieldCount = 16,
            ForeignKeys = 32
        }

        #endregion

        private const int DefaultBatchSize = 5000;

        public class StackOverflowFile
        {
            public string FileName { get; set; }
            public bool IsFound { get; set; }
        }

        public static List<StackOverflowFile> GetStackOverflowFileList()
        {
            List<StackOverflowFile> fileList = new List<StackOverflowFile>();
            fileList.Add(new StackOverflowFile() { FileName = "Badges.xml", IsFound = false });
            fileList.Add(new StackOverflowFile() { FileName = "Comments.xml", IsFound = false });
            fileList.Add(new StackOverflowFile() { FileName = "PostHistory.xml", IsFound = false });
            fileList.Add(new StackOverflowFile() { FileName = "PostLinks.xml", IsFound = false });
            fileList.Add(new StackOverflowFile() { FileName = "Posts.xml", IsFound = false });
            fileList.Add(new StackOverflowFile() { FileName = "Tags.xml", IsFound = false });
            fileList.Add(new StackOverflowFile() { FileName = "Users.xml", IsFound = false });
            fileList.Add(new StackOverflowFile() { FileName = "Votes.xml", IsFound = false });

            return fileList;
        } 

        public Configuration()
        {
            SetDefaults();
        }

        public Configuration(string[] args)
        {
            SetDefaults();
            ParseArguments(args);
        }


        public int BatchSize { get; set; }

        public bool FullText
        {
            get { return (Options & ImportOptions.FullText) == ImportOptions.FullText; }
            set
            {
                Options = value
                              ? Options | ImportOptions.FullText
                              : Options & (ImportOptions)(Options - ImportOptions.FullText);
            }
        }

        public bool GUI
        {
            get { return (Options & ImportOptions.GUI) == ImportOptions.GUI; }
            set
            {
                Options = value
                              ? Options | ImportOptions.GUI
                              : Options & (ImportOptions)(Options - ImportOptions.GUI);
            }
        }

        public bool Indices
        {
            get { return (Options & ImportOptions.Indices) == ImportOptions.Indices; }
            set
            {
                Options = value
                              ? Options | ImportOptions.Indices
                              : Options & (ImportOptions)(Options - ImportOptions.Indices);
            }
        }

        public bool ForeignKeys
        {
            get { return (Options & ImportOptions.ForeignKeys) == ImportOptions.ForeignKeys; }
            set
            {
                Options = value
                              ? Options | ImportOptions.ForeignKeys
                              : Options & (ImportOptions)(Options - ImportOptions.ForeignKeys);
            }
        }

        internal ImportOptions Options { get; set; }

        public DbProviderInfo Provider { get; set; }

        public string Source { get; set; }

        public bool Split
        {
            get { return (Options & ImportOptions.Split) == ImportOptions.Split; }
            set
            {
                Options = value
                              ? Options | ImportOptions.Split
                              : Options & (ImportOptions)(ImportOptions.FieldCount - ImportOptions.Split);
            }
        }

        public string Target { get; set; }

        public List<ImportTarget> Targets { get; set; }


        public Type[] GetAllTableTypes()
        {
            return
                typeof(Posts).Assembly.GetTypes().Where(
                    t => t.Namespace == typeof(Posts).Namespace && (Split || t != typeof(PostTags))).ToArray();
        }

        public string ToString(bool commandLine)
        {
            if (commandLine)
            {
                string targets = string.Join(" ", Targets.Select(t => t.Name == t.Schema ? t.Name : t.Name + ":" + t.Schema).ToArray());
                DbConnectionStringBuilder csb = new DbConnectionStringBuilder();
                csb.ConnectionString = Provider.ConnectionString;
                csb.Add("Provider", Provider.Name);


                string batch = BatchSize != DefaultBatchSize ? " batch:" + BatchSize : "";
                string cmdLine = string.Format("{7} source:\"{0}\" target:\"{1}\"{2}{3}{4}{5} {6}", Source,
                                               csb.ConnectionString, batch, Indices ? " indices" : "",
                                               FullText ? " fulltext" : "", Split ? " split" : "", targets,
                                               Path.GetFileName(Assembly.GetExecutingAssembly().Location));
                return cmdLine;
            }
            else
            {
                return
                    string.Format("Source  : {0}\r\nTarget  : {1}\r\nOptions : {2}", Source, Target, Options);
            }
        }

        public static List<string> GetAllSites(string source)
        {
            List<string> sites = new List<string>();
            string[] dirs = Directory.GetDirectories(source);
            foreach (string dir in dirs)
            {
                List<StackOverflowFile> soFiles = GetStackOverflowFileList();
                foreach (string file in Directory.GetFiles(dir))
                {
                    foreach (StackOverflowFile soFile in soFiles.Where(x => x.FileName == Path.GetFileName(file)))
                    {
                        soFile.IsFound = true;
                    }
                }

                bool soValid = true;
                foreach (StackOverflowFile soFile in soFiles)
                {
                    if (soFile.IsFound == false)
                    {
                        soValid = false;
                        break;
                    }
                }

                if (soValid)
                {
                    sites.Add(Path.GetFileName(dir));
                }
            }
            return sites;

        }

        public static ImportTarget GetSite(string source, string arg)
        {
            return new ImportTarget(arg, Path.Combine(source, arg), Properties.Settings.Default.DefaultSchema);
        }

        public static List<ImportTarget> GetTargets(string source, IEnumerable<string> unparsed)
        {
            List<ImportTarget> targets = new List<ImportTarget>();
            foreach (string arg in unparsed)
            {
                ImportTarget site = GetSite(source, arg);
                if (site != null)
                {
                    targets.Add(site);
                }
            }
            return targets;
        }

        private void ParseArguments(string[] args)
        {
            List<string> unparsed = new List<string>();

            for (int i = 0; i < args.Length; i++)
            {
                string value = null;
                string arg = args[i].Trim();
                if (arg.IndexOf(":") > -1)
                {
                    value = arg.Substring(arg.IndexOf(":") + 1).Trim('"', '\'');
                    arg = arg.Substring(0, arg.IndexOf(":"));
                }
                switch (arg.ToLowerInvariant())
                {
                    case "source":
                        Source = value;
                        break;
                    case "target":
                        Target = value;
                        Provider = DbProviders.ParseArg(value);
                        break;
                    case "batch":
                        int batchSize;
                        if (int.TryParse(value, out batchSize))
                        {
                            BatchSize = batchSize;
                        }
                        break;
                    case "indices":
                        Options = Options | ImportOptions.Indices;
                        break;
                    case "fulltext":
                        Options = Options | ImportOptions.FullText;
                        break;
                    case "split":
                        Options = Options | ImportOptions.Split;
                        break;
                    case "gui":
                        Options = Options | ImportOptions.GUI;
                        break;
                    case "foreignkeys":
                        Options = Options | ImportOptions.ForeignKeys;
                        break;
                    default:
                        unparsed.Add(args[i].Trim());
                        break;
                }
            }

            // unparsed args MUST be sites
            // if no sites specified, get them all
            if (unparsed.Count == 0 && !string.IsNullOrEmpty(Source))
            {
                unparsed = GetAllSites(Source);
            }

            Targets = GetTargets(Source, unparsed);
        }

        private void SetDefaults()
        {
            Targets = new List<ImportTarget>();
            BatchSize = DefaultBatchSize;
        }
    }
}