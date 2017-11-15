using Salient.StackExchange.Import.Loaders;

namespace Salient.StackExchange.Import.TableTypes
{
    public class Tags : SoBase<Tags>, ISoBase
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public int Count { get; set; }
        public int ExcerptPostId { get; set; }
        public int WikiPostId { get; set; }

        public static string GetFileName() => "Tags.xml";
    }
}