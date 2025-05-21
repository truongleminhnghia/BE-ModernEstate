
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class InvetorRequest
    {
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? TaxCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public EnumInvetorType InvetorType { get; set; }
    }
}