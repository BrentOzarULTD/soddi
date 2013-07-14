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

using Salient.StackExchange.Import.Loaders;

#endregion

namespace Salient.StackExchange.Import.TableTypes
{
    public class PostTags : SoBase<PostTags>, ISoBase
    {
        #region Properties

        public int PostId { get; set; }

        public string Tag { get; set; }

        #endregion

        #region Public Methods

        public static string GetFileName()
        {
            return "Posts.xml";
        }

        #endregion
    }
}