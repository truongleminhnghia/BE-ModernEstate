
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("property")]
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

        [Column("original_price", TypeName = "decimal(18,2)")]
        [Required]
        public decimal OriginalPrice { get; set; }

        [Column("sale_price", TypeName = "decimal(18,2)")]
        [Required]
        public decimal SalePrice { get; set; }

        [Column("price_text", TypeName = "varchar(100)")]
        public string? PriceText { get; set; }

        [Column("type_property", TypeName = "varchar(150)")]
        [Required]
        [Description("Type of the property")]
        [EnumDataType(typeof(EnumTypeProperty))]
        public EnumTypeProperty TypeProperty { get; set; }

        [Column("property_area", TypeName = "float")]
        [Description("Area of the property in square meters")]
        [Required]
        public float PropertyArea { get; set; }

        [Column("number_of_bedroom", TypeName = "int")]
        [Description("Number of bedrooms in the property")]
        [Required]
        public int NumberOfBedroom { get; set; }

        [Column("number_of_toilet", TypeName = "int")]
        [Description("Number of toilet in the property")]
        [Required]
        public int NumberOfBathroom { get; set; }

        [Column("number_of_floor", TypeName = "int")]
        [Description("Number of floor in the property")]
        public int NumberOfFloor { get; set; }

        [Column("number_of_room", TypeName = "int")]
        [Description("Number of room of the property")]
        public int NumberOfRoom { get; set; }

        [Column("state", TypeName = "varchar(100)")]
        [Required]
        [Description("State of the property")]
        [EnumDataType(typeof(EnumStateProperty))]
        public EnumStateProperty State { get; set; }

        [Column("status", TypeName = "varchar(100)")]
        [Required]
        [Description("Status of the property")]
        [EnumDataType(typeof(EnumStatusProperty))]
        public EnumStatusProperty Status { get; set; }

        [Column("attribute", TypeName = "JSON")]
        [Description("List of attributes of the property")]
        public string[]? Attribute { get; set; }

        [Column("type_transaction", TypeName = "varchar(100)")]
        [Required]
        [Description("Type of transaction for the property")]
        [EnumDataType(typeof(EnumTypeTransaction))]
        public EnumTypeTransaction TypeTransaction { get; set; }
    }
}