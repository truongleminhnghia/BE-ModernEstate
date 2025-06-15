using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class UpdatePostRequest
    {
        [Description("Source status of the post.")]
        [EnumDataType(typeof(EnumSourceStatus))]
        public EnumSourceStatus? SourceStatus { get; set; }

        [Description("Reason for rejection, if applicable.")]
        public string? RejectionReason { get; set; }

        [Description("Navigation property for the associated property.")]
        public UpdatePropertyRequest? UpdatePropertyRequest { get; set; }

        [Description("Navigation property for the associated contact.")]
        public UpdateContact? Contact { get; set; }

        public PostPackageReuqest? PostPackageReuqest { get; set; }
    }
}