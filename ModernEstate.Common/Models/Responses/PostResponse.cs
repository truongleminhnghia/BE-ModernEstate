
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public EnumStatePost? State { get; set; }
        public EnumSourceStatus SourceStatus { get; set; }
        public string? Description { get; set; }
        public string? RejectionReason { get; set; }
        public PropertyResponse? Property { get; set; }
        public ContactResponse? Contact { get; set; }
        public PostPackageResponse? PostPackage { get; set; }
    }
}