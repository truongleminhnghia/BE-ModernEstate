

using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class PropertyResponse
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
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
        public AddressResponse? Address { get; set; }
        public OwnerPropertyResponse? Owner { get; set; }
        public virtual ICollection<ImageResponse>? PropertyImages { get; set; }
        public virtual ICollection<HistoryResponse>? Histories { get; set; }
        // public virtual ICollection<Post>? Posts { get; set; }
        // public virtual ICollection<Favorite>? Favorites { get; set; }
    }
}