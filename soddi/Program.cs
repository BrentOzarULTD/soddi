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
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Salient.StackExchange.Import.Gui;
using Salient.StackExchange.Import.Loaders;
using Salient.StackExchange.Import.Properties;
using Salient.StackExchange.Import.Tools;
using System.Reflection;

#endregion

namespace Salient.StackExchange.Import
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                
                BulkLoader inserter;
                Console.WriteLine();
                Console.Title = "StackOverflow Data Dump Import v1.5";
                Console.WriteLine(Console.Title);
                Console.WriteLine();
                if(args.ToList().Find(a=>a.ToLowerInvariant()=="help" || a.ToLowerInvariant()=="?")!=null)
                {
                    RenderUsage();
                    return;
                }
                Configuration.Configuration config = new Configuration.Configuration(args);
                Console.WriteLine(config.ToString(false));

                if (config.GUI || args.Length == 0)
                {
                    Application.EnableVisualStyles();
                    Application.Run(new FrmUI(config)); // or whatever
                }
                else
                {
                    inserter = BulkLoader.Create(config);

                    inserter.RowsInserted += (s, e) =>
                        {
                            if (e.Type == CopyEventType.Error)
                            {
                                Console.WriteLine(e.Message);
                            }
                        };

                    Stopwatch sw = new Stopwatch();

                    sw.Start();
                    inserter.ProcessJobs(config);
                    sw.Stop();

                    long count = inserter.Jobs.Select(j => j.Tasks.Sum(t => t.Count)).Sum();
                    Console.WriteLine(Resources.Rs_ImpComplete + "\r\n", count.ToString("#,##0"),
                                      sw.ElapsedMilliseconds / 1000f / 60f);
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("\r\n{0}\r\n",ex.Message);
                Console.WriteLine(ex.StackTrace);
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Console.WriteLine();
                    Console.WriteLine(inner.Message);
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                RenderUsage();
            }
        }

        private static void RenderUsage()
        {
            string usage = Common.GetTextResource(@"README.txt",Assembly.GetExecutingAssembly());
            Console.WriteLine(usage);
        }
    }
}