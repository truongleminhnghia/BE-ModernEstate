using ModernEstate.Common.Enums;


namespace ModernEstate.Common.Models.Requests
{
    public class NewsRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AccountId { get; set; }
        public EnumStatusNew StatusNew { get; set; }
    }
}
