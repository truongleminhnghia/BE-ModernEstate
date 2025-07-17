
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("property")]
    [Index(nameof(Code), IsUnique = true)]
    public class Property : BaseEntity
    {
        [Key]
        [Column("property_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(50)")]
        [Required]
        public string? Code { get; set; }

        [Column("title", TypeName = "varchar(300)")]
        [Required]
        public string? Title { get; set; }

        [Column("description", TypeName = "varchar(1000)")]
        public string? Description { get; set; }

        [Column("demand", TypeName = "varchar(200)")]
        [Required]
        [EnumDataType(typeof(EnumDemand))]
        [Description("Nhu cầu của bất động sản (BÁN hoặc CHO THUÊ)")]
        public EnumDemand Demand { get; set; }

        [Column("attribute", TypeName = "JSON")]
        [Description("List of attributes of the property")]
        public string[]? Attribute { get; set; }


        [Column("type", TypeName = "varchar(200)")]
        [Required]
        [EnumDataType(typeof(EnumTypeProperty))]
        [Description("Loại bất động sản (Căn hộ, Nhà riêng, Đất nền, v.v.)")]
        public EnumTypeProperty Type { get; set; }

        [Column("area")]
        [Required]
        [Description("Diện tích của bất động sản (m2)")]
        public float Area { get; set; }

        [Column("area_unit", TypeName = "varchar(50)")]
        [Required]
        [EnumDataType(typeof(EnumAreaUnit))]
        [Description("Đơn vị diện tích của bất động sản (m2, km2, ha, v.v.)")]
        public EnumAreaUnit AreaUnit { get; set; }

        [Column("price")]
        [Required]
        [Description("Giá của bất động sản (đơn vị: triệu đồng)")]
        public double Price { get; set; }

        [Column("price_unit", TypeName = "varchar(50)")]
        [Required]
        [EnumDataType(typeof(EnumCurrency))]
        [Description("Đơn vị giá của bất động sản (Triệu đồng, Tỷ đồng, USD, v.v.)")]
        public EnumCurrency PriceUnit { get; set; }

        [Column("document", TypeName = "JSON")]
        [Description("Danh sách các tài liệu liên quan đến bất động sản (Hợp đồng, Giấy tờ pháp lý, v.v.)")]
        public string[]? Document { get; set; }

        [Column("interior", TypeName = "varchar(200)")]
        [Description("Nội thất của bất động sản (Có, Không, Hoặc mô tả chi tiết)")]
        public string? Interior { get; set; }

        [Column("number_Of_Bedrooms", TypeName = "int")]
        [Required]
        [Description("Số lượng phòng ngủ của bất động sản")]
        public int NumberOfBedrooms { get; set; }

        [Column("number_of_bathrooms", TypeName = "int")]
        [Required]
        [Description("Số lượng phòng tắm của bất động sản")]
        public int NumberOfBathrooms { get; set; }

        [Column("house_direction", TypeName = "varchar(300)")]
        [Description("Hướng nhà của bất động sản (Bắc, Nam, Đông, Tây, v.v.)")]
        [EnumDataType(typeof(EnumHouseDirection))]
        public EnumHouseDirection? HouseDirection { get; set; }

        [Column("video_url", TypeName = "json")]
        [Description("URL của video giới thiệu bất động sản")]
        public string[]? VideoUrl { get; set; }

        [Column("priority_status", TypeName = "varchar(300)")]
        [Description("Trạng thái ưu tiên của bất động sản (Ưu tiên cao, Trung bình, Thấp)")]
        [EnumDataType(typeof(EnumPriorityStatus))]
        public EnumPriorityStatus? PriorityStatus { get; set; }

        [Column("status", TypeName = "varchar(200)")]
        [Required]
        [EnumDataType(typeof(EnumStatusProperty))]
        [Description("Trạng thái của bất động sản (Đang rao bán, Đã bán, Đang cho thuê, Đã cho thuê, v.v.)")]
        public EnumStatusProperty Status { get; set; }

        [Column("status_source", TypeName = "varchar(200)")]
        [Required]
        [EnumDataType(typeof(EnumSourceStatus))]
        [Description("Nguồn trạng thái của bất động sản (Tự đăng, Đăng bởi môi giới, Đăng bởi người dùng khác, v.v.)")]
        public EnumSourceStatus StatusSource { get; set; }

        [Column("address_id")]
        [Required]
        public Guid AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address? Address { get; set; }

        [Column("owner_id")]
        public Guid? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public OwnerProperty? Owner { get; set; }

        [Column("project_id")]
        public Guid? ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }

        public virtual ICollection<Image>? PropertyImages { get; set; }
        public virtual ICollection<History>? Histories { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
}