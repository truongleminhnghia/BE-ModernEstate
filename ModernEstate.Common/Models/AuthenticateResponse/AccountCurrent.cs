using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.Common.Models.AuthenticateResponse
{
    public class AccountCurrent
    {
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public RoleResponse? Role { get; set; }
        public EnumAccountStatus? EnumAccountStatus { get; set; }
    }
}
