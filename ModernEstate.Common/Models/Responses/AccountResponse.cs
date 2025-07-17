using ModernEstate.Common.Enums;
namespace ModernEstate.Common.Models.Responses
{
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public RoleResponse? Role { get; set; }
        public EnumAccountStatus? EnumAccountStatus { get; set; }
        public BrokerResponse? Broker { get; set; }
        public EmployeeResponse? Employee { get; set; }
        public OwnerPropertyResponse? OwnerProperty { get; set; }
    }
}
