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
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Salient.StackExchange.Import.Configuration;
using Salient.StackExchange.Import.Loaders;
using Salient.StackExchange.Import.Properties;
using Salient.StackExchange.Import.Tools;

#endregion

namespace Salient.StackExchange.Import.Gui
{
    public partial class FrmUI : Form
    {
        private readonly Stopwatch _timer = new Stopwatch();
        private bool _abort;
        private List<ImportTarget> _importTargets;

        public FrmUI(Configuration.Configuration configuration)
        {
            InitializeComponent();
            InitializeUiFromConfig(configuration);
        }

        private void FormLoad(object sender, EventArgs e)
        {
#if !MONO
            TasksListView.SetExStyles();
#endif
            SourceTextChanged(null, EventArgs.Empty);
            sourceTextBox.Focus();
        }

        private void ImportClick(object sender, EventArgs e)
        {
            if (ImportButton.Text == "Abort")
            {
                ImportButton.Text = "Aborting";
                ImportButton.Enabled = false;
                _abort = true;
            }
            else
            {
                if (!ValidateForm())
                {
                    return;
                }

                //_importTargets

                try
                {
                    Configuration.Configuration config = BuildConfigurationFromUi();

                    StartImport(config);
                    panel1.Enabled = false;
                    _abort = false;
                    StatusLabel.Text = "Importing...";
                }
                catch (Exception ex)
                {

                    StatusLabel.Text = ex.Message;
                }
            }
        }


        private Configuration.Configuration BuildConfigurationFromUi()
        {
            _importTargets =
                listView1.CheckedItems.Cast<ListViewItem>().Select(i => i.Tag).Cast<ImportTarget>().ToList();
            Configuration.Configuration tconfig = new Configuration.Configuration
                {
                    Provider = (DbProviderInfo)dbProvidersComboBox.SelectedItem,
                    Source = sourceTextBox.Text,
                    Targets = _importTargets,
                    //Targets =  TargetListBox.CheckedItems.Cast<ImportTarget>().ToList(),
                    BatchSize = (int)BatchSizeNumericUpDown.Value,
                    Split = splitCheckBox.Checked,
                    Indices = indicesCheckBox.Checked,
                    FullText = fullTextCheckBox.Checked,
                    ForeignKeys = fkCheckBox.Checked,
                };
            tconfig.Provider.ConnectionString = targetTextBox.Text;
            return tconfig;
        }


        private void InitializeUiFromConfig(Configuration.Configuration configuration)
        {
            _importTargets = configuration.Targets;

            dbProvidersBindingSource.DataSource = DbProviders.Instance;

            indicesCheckBox.Checked = configuration.Indices;
            splitCheckBox.Checked = configuration.Split;
            fullTextCheckBox.Checked = configuration.FullText;
            BatchSizeNumericUpDown.Value = configuration.BatchSize;

            if (configuration.Provider != null)
            {
                dbProvidersComboBox.SelectedValue = configuration.Provider.Name;
            }
            else
            {
                dbProvidersComboBox.SelectedValue = Settings.Default.ProviderName;
            }
            OnProviderChange();

            sourceTextBox.Text = configuration.Source;

            DbProviderInfo provider = configuration.Provider;

            if (provider != null)
            {
                targetTextBox.Text = provider.ConnectionString;
            }
        }


        private void StartImport(Configuration.Configuration config)
        {
            // clean the plate
            TasksListView.Items.Clear();
            TasksListView.Groups.Clear();

            // build the configuration object and then the loader

            BulkLoader loader = BulkLoader.Create(config);


            // since we want to track progress of tasks, lets
            // get an aggregated list of tasks from the loader's jobs
            List<BulkCopyTask> tasks = new List<BulkCopyTask>();
            loader.Jobs.ForEach(j => j.Tasks.ForEach(tasks.Add));

            // add a list group for each site being loaded.
            // TODO: relocate targets to the loader
            config.Targets.ForEach(t => TasksListView.Groups.Add(new ListViewGroup(t.Name, t.Name)));

            // create the list items and event handler
            tasks.ForEach(t =>
                {
                    ListViewItem item = new ListViewItem();

                    item.SubItems.Add(t.Table);
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");

                    TasksListView.Items.Add(item);
                    item.Group = TasksListView.Groups[t.Site];

                    t.RowsInserted += (ss, eee) => TasksListView.Invoke(() => UpdateTaskItem(t, item, eee));
                });


            // set up the import completion handler
            loader.Jobs.Complete += (ss, ee) => ImportButton.Invoke(() =>
                {
                    ImportButton.Text = "Import";
                    ImportButton.Enabled = true;
                    panel1.Enabled = true;
                    _timer.Stop();
                    long count = loader.Jobs.Select(j => j.Tasks.Sum(t => t.Count)).Sum();


                    StatusLabel.Text =
                        string.Format((_abort ? Resources.Rs_ImpAbort : Resources.Rs_ImpComplete) + "\r\n",
                                      count.ToString("#,##0"),
                                      _timer.ElapsedMilliseconds / 1000f / 60f);
                });


            // start the job

            _timer.Reset();
            _timer.Start();
            ImportButton.Text = "Abort";
            new Thread(() => loader.ProcessJobs(config)).Start();
        }

        private void UpdateTaskItem(BulkCopyTask task, ListViewItem listViewItem, BulkCopyEventArgs args)
        {
            args.Abort = _abort;

            listViewItem.SubItems[2].Text = task.Count.ToString();
            listViewItem.SubItems[3].Text = task.State.ToString();
            listViewItem.SubItems[4].Text = args.Message;

            switch (args.Type)
            {
                case CopyEventType.Active:
                    listViewItem.BackColor = Color.GhostWhite;
                    listViewItem.ForeColor = Color.DarkGreen;
                    break;
                case CopyEventType.Processing:
                    listViewItem.BackColor = Color.LightCyan;
                    listViewItem.ForeColor = Color.DarkBlue;
                    break;
                case CopyEventType.Complete:

                    if (_abort)
                    {
                        listViewItem.BackColor = Color.Pink;
                        listViewItem.ForeColor = Color.DarkRed;
                        listViewItem.SubItems[4].Text = "Aborted";
                    }
                    else
                    {
                        listViewItem.BackColor = Color.LightGreen;
                        listViewItem.ForeColor = Color.DarkGreen;
                    }
                    break;
                case CopyEventType.Error:
                    listViewItem.BackColor = Color.Pink;
                    listViewItem.ForeColor = Color.DarkRed;
                    break;
                default:
                    listViewItem.BackColor = Color.Transparent;
                    listViewItem.ForeColor = Color.Black;
                    break;
            }
        }


        private void ProvidersSelectedIndexChanged(object sender, EventArgs e)
        {
            OnProviderChange();
        }


        private void OnProviderChange()
        {
            DbProviderInfo dbProvider = dbProvidersComboBox.SelectedItem as DbProviderInfo;
            if (dbProvider != null)
            {
                fullTextCheckBox.Enabled = dbProvider.InvariantName == "System.Data.SqlClient";
                if (dbProvider.InvariantName != "System.Data.SqlClient")
                {
                    fullTextCheckBox.Checked = false;
                }
                Settings.Default.ProviderName = dbProvider.Name;
            }
        }

        private void SourceTextChanged(object sender, EventArgs e)
        {
            UpdateSites(sourceTextBox.Text, _importTargets);
        }

        private void UpdateSites(string source, List<ImportTarget> selected)
        {
            listView1.Items.Clear();

            List<ImportTarget> targets = null;
            if (Directory.Exists(source))
            {
                targets = Configuration.Configuration.GetTargets(source, Configuration.Configuration.GetAllSites(source));
            }

            if (targets != null)
            {
                foreach (ImportTarget target in targets)
                {
                    listView1.Items.Add(new ListViewItem(new[] { target.Name, target.Schema })
                        {
                            Tag = target,
                            Checked = selected.Find(t => string.Compare(t.Path, target.Path, true) == 0) != null
                        });
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Settings.Default.Save();
            base.OnClosing(e);
        }

        private void buildCommandLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCmdLine frm = new FrmCmdLine(BuildConfigurationFromUi());
            frm.ShowDialog();
        }


        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ImportTarget target = (ImportTarget)listView1.Items[e.Item].Tag;
            target.Schema = e.Label;
        }

        private bool ValidateForm()
        {
            bool valid = true;
            errorProvider1.Clear();
            if (listView1.Items.Count == 0)
            {
                valid = false;
                errorProvider1.SetError(sourceTextBox, "Invalid source path.");
            }
            if (listView1.CheckedItems.Count == 0)
            {
                valid = false;
                errorProvider1.SetError(listView1, "No sites selected.");
            }

            try
            {
                DbProviderInfo info = (DbProviderInfo)dbProvidersComboBox.SelectedItem;
                DbConnectionStringBuilder csb = info.Factory.CreateConnectionStringBuilder();
                csb.ConnectionString = targetTextBox.Text;
                targetTextBox.Text = csb.ConnectionString;
            }
            catch (Exception ex)
            {
                valid = false;
                errorProvider1.SetError(targetTextBox, ex.Message);
            }

            return valid;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAbout frm = new FrmAbout();
            frm.Show();
        }

        private void BrowseSourceButton_Click(object sender, EventArgs e)
        {
            var bfd = new FolderBrowserDialog();
            if (bfd.ShowDialog() == DialogResult.OK)
            {
                sourceTextBox.Text = bfd.SelectedPath;
            }
        }

        private void splitCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}