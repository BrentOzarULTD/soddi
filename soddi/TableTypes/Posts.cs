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
using Salient.StackExchange.Import.Loaders;

#endregion

namespace Salient.StackExchange.Import.TableTypes
{
    public class Posts : SoBase<Posts>, ISoBase
    {
        #region Properties

        public int AcceptedAnswerId { get; set; }

        public int AnswerCount { get; set; }

        public string Body { get; set; }

        public DateTime? ClosedDate { get; set; }

        public int CommentCount { get; set; }

        public DateTime? CommunityOwnedDate { get; set; }

        public DateTime CreationDate { get; set; }

        public int FavoriteCount { get; set; }

        public int Id { get; set; }

        public DateTime LastActivityDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public string LastEditorDisplayName { get; set; }

        public int LastEditorUserId { get; set; }

        public int OwnerUserId { get; set; }

        public int ParentId { get; set; }

        public int PostTypeId { get; set; }

        public int Score { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }

        public int ViewCount { get; set; }

        #endregion

        #region Public Methods

        public static string GetFileName()
        {
            return "Posts.xml";
        }

        #endregion
    }
}