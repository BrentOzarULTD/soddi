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
using System.Reflection;
using System.Windows.Forms;
using Salient.StackExchange.Import.Tools;

#endregion

namespace Salient.StackExchange.Import.Gui
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Common.GetTextResource(@"README.txt", Assembly.GetExecutingAssembly());
        }
    }
}