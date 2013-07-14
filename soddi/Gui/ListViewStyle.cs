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
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace Salient.StackExchange.Import.Gui
{
    /// <summary>
    /// Exposes styles hidden by .net managed wrapper
    /// </summary>
    /// Adapted from http://www.codeproject.com/KB/list/listviewxp.aspx
    public static class ListViewStyle
    {
#if !MONO

        #region LVM enum

        public enum LVM
        {
            LVMFirst = 0x1000,
            LVMSetextendedlistviewstyle = (LVMFirst + 54),
            LVMGetextendedlistviewstyle = (LVMFirst + 55),
        }

        #endregion

        #region LvsEx enum

        [Flags]
        public enum LvsEx
        {
            LvsExGridlines = 0x00000001,
            LvsExSubitemimages = 0x00000002,
            LvsExCheckboxes = 0x00000004,
            LvsExTrackselect = 0x00000008,
            LvsExHeaderdragdrop = 0x00000010,
            LvsExFullrowselect = 0x00000020,
            LvsExOneclickactivate = 0x00000040,
            LvsExTwoclickactivate = 0x00000080,
            LvsExFlatsb = 0x00000100,
            LvsExRegional = 0x00000200,
            LvsExInfotip = 0x00000400,
            LvsExUnderlinehot = 0x00000800,
            LvsExUnderlinecold = 0x00001000,
            LvsExMultiworkareas = 0x00002000,
            LvsExLabeltip = 0x00004000,
            LvsExBorderselect = 0x00008000,
            LvsExDoublebuffer = 0x00010000,
            LvsExHidelabels = 0x00020000,
            LvsExSinglerow = 0x00040000,
            LvsExSnaptogrid = 0x00080000,
            LvsExSimpleselect = 0x00100000
        }

        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr handle, int messg, int wparam, int lparam);

        /// <summary>
        /// Sets Double_Buffering and BorderSelect style
        /// </summary>
        /// <param name="lv"></param>
        public static void SetExStyles(this ListView lv)
        {
            LvsEx styles = (LvsEx) SendMessage(lv.Handle, (int) LVM.LVMGetextendedlistviewstyle, 0, 0);
            styles |= LvsEx.LvsExDoublebuffer | LvsEx.LvsExBorderselect;
            SendMessage(lv.Handle, (int) LVM.LVMSetextendedlistviewstyle, 0, (int) styles);
        }

        /// <summary>
        /// Sets ListViewExtended Styles
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="exStyle">The Styles you wish to set.</param>
        public static void SetExStyles(this ListView lv, LvsEx exStyle)
        {
            LvsEx styles = (LvsEx) SendMessage(lv.Handle, (int) LVM.LVMGetextendedlistviewstyle, 0, 0);
            styles |= exStyle;
            SendMessage(lv.Handle, (int) LVM.LVMSetextendedlistviewstyle, 0, (int) styles);
        }
#endif
    }

}