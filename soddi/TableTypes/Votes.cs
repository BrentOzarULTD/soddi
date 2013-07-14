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
    public class Votes : SoBase<Votes>, ISoBase
    {
        #region Properties

        public int? BountyAmount { get; set; }

        public DateTime CreationDate { get; set; }

        public int Id { get; set; }

        public int PostId { get; set; }

        public int? UserId { get; set; }

        public int VoteTypeId { get; set; }

        #endregion

        #region Public Methods

        public static string GetFileName()
        {
            return "Votes.xml";
        }

        #endregion
    }
}