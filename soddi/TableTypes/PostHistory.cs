using System;
using Salient.StackExchange.Import.Loaders;

namespace Salient.StackExchange.Import.TableTypes
{
    public class PostHistory : SoBase<PostHistory>, ISoBase
    {
        public int Id { get; set; }
        public int PostHistoryTypeId { get; set; }
        public int PostId { get; set; }
        public Guid RevisionGUID { get; set; }
        public DateTime CreationDate { get; set; }
        public int? UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string Comment { get; set; }
        public string Text { get; set; }

        public static string GetFileName() => "PostHistory.xml";
    }
}