using ModernEstate.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Common.Models.Requests
{
    public class AccountRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }

        [Required]
        [EnumDataType(typeof(EnumRoleName))]
        public EnumRoleName RoleName { get; set; }

        [Required]
        [EnumDataType(typeof(EnumAccountStatus))]
        public EnumAccountStatus? EnumAccountStatus { get; set; }

        [Required]
        [EnumDataType(typeof(EnumGender))]
        public EnumGender Gender { get; set; }
    }
}
