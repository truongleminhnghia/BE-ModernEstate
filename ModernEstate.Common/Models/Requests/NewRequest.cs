using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class NewRequest
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public EnumStatusNew StatusNew { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid? AccountId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
