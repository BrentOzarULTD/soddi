using Salient.StackExchange.Import.Configuration;

namespace Salient.StackExchange.Import.Gui
{
    partial class FrmUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label bulkInsertBatchSizeLabel;
            System.Windows.Forms.Label sourceLabel;
            System.Windows.Forms.Label targetLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUI));
            this.BatchSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.fullTextCheckBox = new System.Windows.Forms.CheckBox();
            this.indicesCheckBox = new System.Windows.Forms.CheckBox();
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.splitCheckBox = new System.Windows.Forms.CheckBox();
            this.targetTextBox = new System.Windows.Forms.TextBox();
            this.ImportButton = new System.Windows.Forms.Button();
            this.dbProvidersComboBox = new System.Windows.Forms.ComboBox();
            this.dbProvidersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TasksListView = new System.Windows.Forms.ListView();
            this.SiteColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TableColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CountColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MessageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.buildCommandLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SchemaColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SiteNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.fkCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BrowseSourceButton = new System.Windows.Forms.Button();
            bulkInsertBatchSizeLabel = new System.Windows.Forms.Label();
            sourceLabel = new System.Windows.Forms.Label();
            targetLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BatchSizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbProvidersBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bulkInsertBatchSizeLabel
            // 
            bulkInsertBatchSizeLabel.AutoSize = true;
            bulkInsertBatchSizeLabel.Location = new System.Drawing.Point(294, 75);
            bulkInsertBatchSizeLabel.Name = "bulkInsertBatchSizeLabel";
            bulkInsertBatchSizeLabel.Size = new System.Drawing.Size(61, 13);
            bulkInsertBatchSizeLabel.TabIndex = 1;
            bulkInsertBatchSizeLabel.Text = "Batch Size:";
            // 
            // sourceLabel
            // 
            sourceLabel.AutoSize = true;
            sourceLabel.Location = new System.Drawing.Point(3, 0);
            sourceLabel.Name = "sourceLabel";
            sourceLabel.Size = new System.Drawing.Size(44, 13);
            sourceLabel.TabIndex = 9;
            sourceLabel.Text = "Source:";
            // 
            // targetLabel
            // 
            targetLabel.AutoSize = true;
            targetLabel.Location = new System.Drawing.Point(3, 36);
            targetLabel.Name = "targetLabel";
            targetLabel.Size = new System.Drawing.Size(41, 13);
            targetLabel.TabIndex = 13;
            targetLabel.Text = "Target:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(0, 75);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 13);
            label1.TabIndex = 20;
            label1.Text = "Provider:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(397, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(33, 13);
            label2.TabIndex = 22;
            label2.Text = "Sites:";
            this.toolTip1.SetToolTip(label2, "Checked sites will be imported.\r\nEdit Schema label to change target.\r\n");
            // 
            // BatchSizeNumericUpDown
            // 
            this.BatchSizeNumericUpDown.Location = new System.Drawing.Point(297, 92);
            this.BatchSizeNumericUpDown.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.BatchSizeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BatchSizeNumericUpDown.Name = "BatchSizeNumericUpDown";
            this.BatchSizeNumericUpDown.Size = new System.Drawing.Size(86, 20);
            this.BatchSizeNumericUpDown.TabIndex = 5;
            this.toolTip1.SetToolTip(this.BatchSizeNumericUpDown, "Number of rows per transaction.");
            this.BatchSizeNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fullTextCheckBox
            // 
            this.fullTextCheckBox.Checked = global::Salient.StackExchange.Import.Properties.Settings.Default.FullTextChecked;
            this.fullTextCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Salient.StackExchange.Import.Properties.Settings.Default, "FullTextChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fullTextCheckBox.Location = new System.Drawing.Point(169, 120);
            this.fullTextCheckBox.Name = "fullTextCheckBox";
            this.fullTextCheckBox.Size = new System.Drawing.Size(72, 24);
            this.fullTextCheckBox.TabIndex = 8;
            this.fullTextCheckBox.Text = "Full Text";
            this.toolTip1.SetToolTip(this.fullTextCheckBox, "Sql Server only - Install full text index on Posts.Body and Posts.Title");
            this.fullTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // indicesCheckBox
            // 
            this.indicesCheckBox.Checked = global::Salient.StackExchange.Import.Properties.Settings.Default.IndicesChecked;
            this.indicesCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Salient.StackExchange.Import.Properties.Settings.Default, "IndicesChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.indicesCheckBox.Location = new System.Drawing.Point(95, 120);
            this.indicesCheckBox.Name = "indicesCheckBox";
            this.indicesCheckBox.Size = new System.Drawing.Size(68, 24);
            this.indicesCheckBox.TabIndex = 7;
            this.indicesCheckBox.Text = "Indices";
            this.toolTip1.SetToolTip(this.indicesCheckBox, "Define useful indexes to improve database performance");
            this.indicesCheckBox.UseVisualStyleBackColor = true;
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Salient.StackExchange.Import.Properties.Settings.Default, "SourceValue", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.sourceTextBox.Location = new System.Drawing.Point(3, 13);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(352, 20);
            this.sourceTextBox.TabIndex = 1;
            this.sourceTextBox.Text = global::Salient.StackExchange.Import.Properties.Settings.Default.SourceValue;
            this.toolTip1.SetToolTip(this.sourceTextBox, "The path of the decompressed data dump.");
            this.sourceTextBox.TextChanged += new System.EventHandler(this.SourceTextChanged);
            // 
            // splitCheckBox
            // 
            this.splitCheckBox.Checked = global::Salient.StackExchange.Import.Properties.Settings.Default.SplitChecked;
            this.splitCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Salient.StackExchange.Import.Properties.Settings.Default, "SplitChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitCheckBox.Location = new System.Drawing.Point(3, 120);
            this.splitCheckBox.Name = "splitCheckBox";
            this.splitCheckBox.Size = new System.Drawing.Size(86, 24);
            this.splitCheckBox.TabIndex = 6;
            this.splitCheckBox.Text = "Create Tags";
            this.toolTip1.SetToolTip(this.splitCheckBox, "Split tags into a separate table");
            this.splitCheckBox.UseVisualStyleBackColor = true;
            this.splitCheckBox.CheckedChanged += new System.EventHandler(this.splitCheckBox_CheckedChanged);
            // 
            // targetTextBox
            // 
            this.targetTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Salient.StackExchange.Import.Properties.Settings.Default, "TargetText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.targetTextBox.Location = new System.Drawing.Point(3, 52);
            this.targetTextBox.Name = "targetTextBox";
            this.targetTextBox.Size = new System.Drawing.Size(383, 20);
            this.targetTextBox.TabIndex = 3;
            this.targetTextBox.Text = global::Salient.StackExchange.Import.Properties.Settings.Default.TargetText;
            this.toolTip1.SetToolTip(this.targetTextBox, resources.GetString("targetTextBox.ToolTip"));
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(9, 178);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(383, 36);
            this.ImportButton.TabIndex = 10;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportClick);
            // 
            // dbProvidersComboBox
            // 
            this.dbProvidersComboBox.DataSource = this.dbProvidersBindingSource;
            this.dbProvidersComboBox.DisplayMember = "Name";
            this.dbProvidersComboBox.FormattingEnabled = true;
            this.dbProvidersComboBox.Location = new System.Drawing.Point(0, 91);
            this.dbProvidersComboBox.Name = "dbProvidersComboBox";
            this.dbProvidersComboBox.Size = new System.Drawing.Size(273, 21);
            this.dbProvidersComboBox.TabIndex = 4;
            this.dbProvidersComboBox.ValueMember = "Name";
            this.dbProvidersComboBox.SelectedIndexChanged += new System.EventHandler(this.ProvidersSelectedIndexChanged);
            // 
            // dbProvidersBindingSource
            // 
            this.dbProvidersBindingSource.DataSource = typeof(Salient.StackExchange.Import.Configuration.DbProviderInfo);
            // 
            // TasksListView
            // 
            this.TasksListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TasksListView.CausesValidation = false;
            this.TasksListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SiteColumnHeader,
            this.TableColumnHeader,
            this.CountColumnHeader,
            this.StateColumnHeader,
            this.MessageColumnHeader});
            this.TasksListView.Location = new System.Drawing.Point(9, 220);
            this.TasksListView.Name = "TasksListView";
            this.TasksListView.Size = new System.Drawing.Size(599, 171);
            this.TasksListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.TasksListView.TabIndex = 21;
            this.TasksListView.TabStop = false;
            this.TasksListView.UseCompatibleStateImageBehavior = false;
            this.TasksListView.View = System.Windows.Forms.View.Details;
            // 
            // SiteColumnHeader
            // 
            this.SiteColumnHeader.Text = "";
            this.SiteColumnHeader.Width = 0;
            // 
            // TableColumnHeader
            // 
            this.TableColumnHeader.Text = "Table";
            this.TableColumnHeader.Width = 80;
            // 
            // CountColumnHeader
            // 
            this.CountColumnHeader.Text = "Count";
            this.CountColumnHeader.Width = 80;
            // 
            // StateColumnHeader
            // 
            this.StateColumnHeader.Text = "State";
            this.StateColumnHeader.Width = 70;
            // 
            // MessageColumnHeader
            // 
            this.MessageColumnHeader.Text = "";
            this.MessageColumnHeader.Width = 320;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(589, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildCommandLineToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(589, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // buildCommandLineToolStripMenuItem
            // 
            this.buildCommandLineToolStripMenuItem.Name = "buildCommandLineToolStripMenuItem";
            this.buildCommandLineToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.buildCommandLineToolStripMenuItem.Text = "Build Command Line";
            this.buildCommandLineToolStripMenuItem.Click += new System.EventHandler(this.buildCommandLineToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(82, 20);
            this.toolStripMenuItem1.Text = "Help/About";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SchemaColumnHeader,
            this.SiteNameColumnHeader});
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(400, 13);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(163, 117);
            this.listView1.TabIndex = 9;
            this.toolTip1.SetToolTip(this.listView1, "Checked sites will be imported.\r\nEdit Schema label to change target.\r\n");
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
            // 
            // SchemaColumnHeader
            // 
            this.SchemaColumnHeader.Text = "Schema";
            this.SchemaColumnHeader.Width = 70;
            // 
            // SiteNameColumnHeader
            // 
            this.SiteNameColumnHeader.Text = "Site";
            this.SiteNameColumnHeader.Width = 50;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // fkCheckBox
            // 
            this.fkCheckBox.Checked = global::Salient.StackExchange.Import.Properties.Settings.Default.FKChecked;
            this.fkCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Salient.StackExchange.Import.Properties.Settings.Default, "FKChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fkCheckBox.Location = new System.Drawing.Point(247, 120);
            this.fkCheckBox.Name = "fkCheckBox";
            this.fkCheckBox.Size = new System.Drawing.Size(92, 24);
            this.fkCheckBox.TabIndex = 23;
            this.fkCheckBox.Text = "Foreign Keys";
            this.toolTip1.SetToolTip(this.fkCheckBox, "Sql Server only - Create Foreign Keys  into the schema.");
            this.fkCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fkCheckBox);
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.BrowseSourceButton);
            this.panel1.Controls.Add(this.dbProvidersComboBox);
            this.panel1.Controls.Add(this.BatchSizeNumericUpDown);
            this.panel1.Controls.Add(this.sourceTextBox);
            this.panel1.Controls.Add(this.targetTextBox);
            this.panel1.Controls.Add(sourceLabel);
            this.panel1.Controls.Add(targetLabel);
            this.panel1.Controls.Add(this.splitCheckBox);
            this.panel1.Controls.Add(this.indicesCheckBox);
            this.panel1.Controls.Add(this.fullTextCheckBox);
            this.panel1.Controls.Add(bulkInsertBatchSizeLabel);
            this.panel1.Controls.Add(label1);
            this.panel1.Controls.Add(label2);
            this.panel1.Location = new System.Drawing.Point(9, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 145);
            this.panel1.TabIndex = 26;
            // 
            // BrowseSourceButton
            // 
            this.BrowseSourceButton.Location = new System.Drawing.Point(361, 12);
            this.BrowseSourceButton.Name = "BrowseSourceButton";
            this.BrowseSourceButton.Size = new System.Drawing.Size(25, 23);
            this.BrowseSourceButton.TabIndex = 2;
            this.BrowseSourceButton.Text = "...";
            this.BrowseSourceButton.UseVisualStyleBackColor = true;
            this.BrowseSourceButton.Click += new System.EventHandler(this.BrowseSourceButton_Click);
            // 
            // FrmUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(584, 507);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.TasksListView);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 1024);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FrmUI";
            this.Text = "SODDI v.10";
            this.Load += new System.EventHandler(this.FormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.BatchSizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbProvidersBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown BatchSizeNumericUpDown;
        private System.Windows.Forms.CheckBox fullTextCheckBox;
        private System.Windows.Forms.CheckBox indicesCheckBox;
        private System.Windows.Forms.TextBox sourceTextBox;
        private System.Windows.Forms.CheckBox splitCheckBox;
        private System.Windows.Forms.TextBox targetTextBox;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.ComboBox dbProvidersComboBox;
        private System.Windows.Forms.ListView TasksListView;
        private System.Windows.Forms.ColumnHeader SiteColumnHeader;
        private System.Windows.Forms.ColumnHeader CountColumnHeader;
        private System.Windows.Forms.ColumnHeader TableColumnHeader;
        private System.Windows.Forms.ColumnHeader StateColumnHeader;
        private System.Windows.Forms.ColumnHeader MessageColumnHeader;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripMenuItem buildCommandLineToolStripMenuItem;
        private System.Windows.Forms.BindingSource dbProvidersBindingSource;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader SiteNameColumnHeader;
        private System.Windows.Forms.ColumnHeader SchemaColumnHeader;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BrowseSourceButton;
        private System.Windows.Forms.CheckBox fkCheckBox;
    }
}