

namespace ModernEstate.Common.Models.Responses
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string? HouseNumber { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? AddressDetail { get; set; }
    }
}