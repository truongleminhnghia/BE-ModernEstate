
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? AppRovedBy { get; set; }
        public string PostBy { get; set; } = string.Empty;

        [Description("State of the post.")]
        [EnumDataType(typeof(EnumDemand))]
        public EnumDemand Demand { get; set; }

        [Description("Source status of the post.")]
        [EnumDataType(typeof(EnumSourceStatus))]
        public EnumSourceStatus SourceStatus { get; set; }

        [Description("Reason for rejection, if applicable.")]
        public string? RejectionReason { get; set; }

        [Description("Status of the post (ACTIVE, INACTIVE).")]
        [EnumDataType(typeof(EnumStatus))]
        public EnumStatus Status { get; set; }

        [Description("Navigation property for the associated property.")]
        public PropertyResponse? Property { get; set; }

        [Description("Navigation property for the associated contact.")]
        public ContactResponse? Contact { get; set; }

        public virtual ICollection<PostPackageResponse>? PostPackages { get; set; }
        // public virtual ICollection<History>? Histories { get; set; }
    }
}