using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class RegisterRequest
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public EnumRoleName? RoleName { get; set; }
    }
}
