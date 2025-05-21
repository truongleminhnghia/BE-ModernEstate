
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PropertyRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string? PriceText { get; set; }
        public EnumTypeProperty TypeProperty { get; set; }
        public float PropertyArea { get; set; }
        public int NumberOfBedroom { get; set; }
        public int NumberOfBathroom { get; set; }
        public int NumberOfFloor { get; set; }
        public int NumberOfRoom { get; set; }
        public EnumStateProperty State { get; set; }
        public EnumStatusProperty Status { get; set; }
        public string[]? Attribute { get; set; }
        public AddressRequest? AddressRequest { get; set; }
        public OwnerPropertyRequest? OwnerPropertyRequest { get; set; }
        public List<ImageRequest>? PropertyImages { get; set; }
    }
}