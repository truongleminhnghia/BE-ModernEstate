
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
        public EnumProjectStatus Status { get; set; }
        public AddressRequest? AddressRequest { get; set; }
        public InvetorRequest? InvetorRequest { get; set; }
    }
}