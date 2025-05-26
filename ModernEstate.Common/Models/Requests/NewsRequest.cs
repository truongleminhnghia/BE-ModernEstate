using ModernEstate.Common.Enums;


namespace ModernEstate.Common.Models.Requests
{
    public class NewsRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public List<String> TagNames { get; set; }
        public EnumStatusNew StatusNew { get; set; }
    }
}
