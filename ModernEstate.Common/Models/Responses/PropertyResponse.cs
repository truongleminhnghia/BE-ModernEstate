

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class PropertyResponse
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        [Description("Nhu cầu của bất động sản (BÁN hoặc CHO THUÊ)")]
        public EnumDemand Demand { get; set; }

        [Description("List of attributes of the property")]
        public string[]? Attribute { get; set; }

        [EnumDataType(typeof(EnumTypeProperty))]
        [Description("Loại bất động sản (Căn hộ, Nhà riêng, Đất nền, v.v.)")]
        public EnumTypeProperty Type { get; set; }

        [Description("Diện tích của bất động sản (m2)")]
        public float Area { get; set; }

        [EnumDataType(typeof(EnumAreaUnit))]
        [Description("Đơn vị diện tích của bất động sản (m2, km2, ha, v.v.)")]
        public EnumAreaUnit AreaUnit { get; set; }

        [Description("Giá của bất động sản (đơn vị: triệu đồng)")]
        public double Price { get; set; }

        [EnumDataType(typeof(EnumCurrency))]
        [Description("Đơn vị giá của bất động sản (Triệu đồng, Tỷ đồng, USD, v.v.)")]
        public EnumCurrency PriceUnit { get; set; }

        [Description("Danh sách các tài liệu liên quan đến bất động sản (Hợp đồng, Giấy tờ pháp lý, v.v.)")]
        public string[]? Document { get; set; }

        [Description("Nội thất của bất động sản (Có, Không, Hoặc mô tả chi tiết)")]
        public string? Interior { get; set; }

        [Description("Số lượng phòng ngủ của bất động sản")]
        public int NumberOfBedrooms { get; set; }

        [Description("Số lượng phòng tắm của bất động sản")]
        public int NumberOfBathrooms { get; set; }

        [Description("Hướng nhà của bất động sản (Bắc, Nam, Đông, Tây, v.v.)")]
        [EnumDataType(typeof(EnumHouseDirection))]
        public EnumHouseDirection? HouseDirection { get; set; }

        [Description("URL của video giới thiệu bất động sản")]
        public string[]? VideoUrl { get; set; }

        [Description("Trạng thái ưu tiên của bất động sản (Ưu tiên cao, Trung bình, Thấp)")]
        [EnumDataType(typeof(EnumPriorityStatus))]
        public EnumPriorityStatus? PriorityStatus { get; set; }

        [EnumDataType(typeof(EnumStatusProperty))]
        [Description("Trạng thái của bất động sản (Đang rao bán, Đã bán, Đang cho thuê, Đã cho thuê, v.v.)")]
        public EnumStatusProperty Status { get; set; }

        [EnumDataType(typeof(EnumSourceStatus))]
        [Description("Nguồn trạng thái của bất động sản (Tự đăng, Đăng bởi môi giới, Đăng bởi người dùng khác, v.v.)")]
        public EnumSourceStatus StatusSource { get; set; }
        public AddressResponse? Address { get; set; }

        [EnumDataType(typeof(EnumTypePackage))]
        public EnumTypePackage PackageName { get; set; }

        public OwnerPropertyResponse? Owner { get; set; }

        public ProjectResponse? Project { get; set; }

        public virtual ICollection<ImageResponse>? PropertyImages { get; set; }

        public virtual ICollection<HistoryResponse>? Histories { get; set; }

    }
}