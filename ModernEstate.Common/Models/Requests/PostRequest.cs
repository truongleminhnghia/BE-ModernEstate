

using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PostRequest
    {
        public string? Title { get; set; }
        public EnumStatePost? State { get; set; }
        public string? Description { get; set; }
        public string? RejectionReason { get; set; }
        public Guid? PropertyId { get; set; }
        public PropertyRequest? Property { get; set; }
        public ContactRequest? Contact { get; set; }
        public PostPackageReuqest? PostPackage { get; set; }
    }
}