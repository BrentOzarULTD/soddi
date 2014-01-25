/*
 * Copyright 2014, Jeremiah Peschka
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://skysanders.net/tools/se/LICENSE.TXT
 */

using System;
using Salient.StackExchange.Import.Loaders;

namespace Salient.StackExchange.Import.TableTypes
{
    public class PostLinks : SoBase<PostLinks>, ISoBase
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostId { get; set; }
        public int RelatedPostId { get; set; }
        public Int16 LinkTypeId { get; set; }

        public static string GetFileName()
        {
            return "PostLinks.xml";
        }
    }
}
