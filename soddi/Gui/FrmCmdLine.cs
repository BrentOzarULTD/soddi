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

using System.Windows.Forms;

#endregion

namespace Salient.StackExchange.Import.Gui
{
    public partial class FrmCmdLine : Form
    {
        public FrmCmdLine(Configuration.Configuration config)
        {
            InitializeComponent();
            textBox1.Text = config.ToString(true);
        }
    }
}