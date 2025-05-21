

using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public EnumProjectType TypeProject { get; set; }
        public int TotalBlock { get; set; }
        public string[]? BlockName { get; set; }
        public int TotalFloor { get; set; }
        public string? Title { get; set; }
        public float ProjectArea { get; set; }
        public EnumProjectStatus Status { get; set; }
        public Guid AddressId { get; set; }
        public AddressResponse? Address { get; set; }
        public Guid InvetorId { get; set; }
        public InvetorResponse? Invetor { get; set; }

        // public virtual ICollection<Property>? Properties { get; set; }
        public virtual ICollection<HistoryResponse>? Histories { get; set; }
        public virtual ICollection<ImageResponse>? Images { get; set; }
    }
}