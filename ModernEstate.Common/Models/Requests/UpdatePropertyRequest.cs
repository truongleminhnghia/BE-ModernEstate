
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class UpdatePropertyRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string[]? Attribute { get; set; }
        public double? Price { get; set; }
        public EnumCurrency? PriceUnit { get; set; }
        public string[]? Document { get; set; }
        public string? Interior { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public int? NumberOfBathrooms { get; set; }
        public EnumHouseDirection? HouseDirection { get; set; }
        public string[]? VideoUrl { get; set; }
        public List<ImageRequest>? Images { get; set; }
    }
}