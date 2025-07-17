
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PropertyRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Description("List of attributes of the property")]
        public string[]? Attribute { get; set; }

        [Required]
        [EnumDataType(typeof(EnumTypeProperty))]
        [Description("Loại bất động sản (Căn hộ, Nhà riêng, Đất nền, v.v.)")]
        public EnumTypeProperty Type { get; set; }

        [Required]
        [Description("Diện tích của bất động sản (m2)")]
        public float Area { get; set; }

        [Required]
        [EnumDataType(typeof(EnumAreaUnit))]
        [Description("Đơn vị diện tích của bất động sản (m2, km2, ha, v.v.)")]
        public EnumAreaUnit AreaUnit { get; set; }

        [Required]
        [Description("Giá của bất động sản (đơn vị: triệu đồng)")]
        public double Price { get; set; }

        [Required]
        [EnumDataType(typeof(EnumCurrency))]
        [Description("Đơn vị giá của bất động sản (Triệu đồng, Tỷ đồng, USD, v.v.)")]
        public EnumCurrency PriceUnit { get; set; }

        [Description("Danh sách các tài liệu liên quan đến bất động sản (Hợp đồng, Giấy tờ pháp lý, v.v.)")]
        public string[]? Document { get; set; }

        [Description("Nội thất của bất động sản (Có, Không, Hoặc mô tả chi tiết)")]
        public string? Interior { get; set; }

        [Required]
        [Description("Số lượng phòng ngủ của bất động sản")]
        public int NumberOfBedrooms { get; set; }

        [Required]
        [Description("Số lượng phòng tắm của bất động sản")]
        public int NumberOfBathrooms { get; set; }

        [Description("Hướng nhà của bất động sản (Bắc, Nam, Đông, Tây, v.v.)")]
        [EnumDataType(typeof(EnumHouseDirection))]
        public EnumHouseDirection? HouseDirection { get; set; }

        [Description("URL của video giới thiệu bất động sản")]
        public string[]? VideoUrl { get; set; }

        public AddressRequest? Address { get; set; }
        public Guid? ProjectId { get; set; }

        public List<ImageRequest>? Images { get; set; }
    }
}