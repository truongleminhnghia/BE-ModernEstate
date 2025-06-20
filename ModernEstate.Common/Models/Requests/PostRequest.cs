

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PostRequest
    {
        [Required(ErrorMessage = "Mã bài đăng là bắt buộc.")]
        public string PostBy { get; set; } = string.Empty;

        [Required]
        [Description("State of the post.")]
        [EnumDataType(typeof(EnumDemand))]
        public EnumDemand Demand { get; set; }

        [Description("Navigation property for the associated property.")]
        [Required(ErrorMessage = "Thông tin bất động sản là bắt buộc.")]
        public PropertyRequest? NewProperty { get; set; }

        [Description("Navigation property for the associated contact.")]
        [Required(ErrorMessage = "Thông tin liên hệ là bắt buộc.")]
        public ContactRequest? Contact { get; set; }

        [Required(ErrorMessage = "Post package ko được null")]
        [Description("Source status of the post.")]
        public PostPackageReuqest? PostPackagesRequest { get; set; }
    }
}