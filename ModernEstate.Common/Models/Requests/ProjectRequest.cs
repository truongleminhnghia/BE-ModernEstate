
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class ProjectRequest
    {
        public string? Title { get; set; }
        public EnumProjectType TypeProject { get; set; }
        public int? TotalBlock { get; set; }
        public string[]? BlockName { get; set; }
        public int? TotalFloor { get; set; }
        public float? ProjectArea { get; set; }
        public string[]? Attribute { get; set; }
        public DateTime? TimeStart { get; set; }
        public double? PriceMin { get; set; }
        public double? PriceMax { get; set; }
        public string? UnitArea { get; set; }
        public EnumCurrency? UnitCurrency { get; set; }
        public string? Description { get; set; }
        public double? TotalInvestment { get; set; }
        public EnumProjectStatus Status { get; set; }
        public AddressRequest? AddressRequest { get; set; }
        public InvetorRequest? InvetorRequest { get; set; }
        public List<ImageRequest>? ImageRequests { get; set; }
    }
}