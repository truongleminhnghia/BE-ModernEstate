
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("project")]
    [Description("Project entity representing a real estate project")]
    [Comment("Project entity representing a real estate project")]
    [Index(nameof(Code), IsUnique = true)]
    [Index(nameof(TypeProject))]
    [Index(nameof(Status))]
    [Index(nameof(ProjectArea))]
    public class Project : BaseEntity
    {
        [Key]
        [Column("project_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(50)")]
        [Required]
        public string? Code { get; set; }

        [Column("type_project", TypeName = "varchar(100)")]
        [Required]
        [EnumDataType(typeof(EnumProjectType))]
        public EnumProjectType TypeProject { get; set; }

        [Column("total_block", TypeName = "int")]
        [Description("Total number of blocks in the project")]
        public int TotalBlock { get; set; }

        [Column("block_name", TypeName = "JSON")]
        [Description("List of block names in the project")]
        public string[]? BlockName { get; set; }

        [Column("total_floor", TypeName = "int")]
        [Description("Total number of floors in the project")]
        public int TotalFloor { get; set; }

        [Column("title", TypeName = "varchar(100)")]
        [Description("Title of the project")]
        public string? Title { get; set; }

        [Column("project_area", TypeName = "float")]
        [Description("Total area of the project in square meters")]
        public float ProjectArea { get; set; }

        [Column("status", TypeName = "varchar(100)")]
        [Required]
        [EnumDataType(typeof(EnumProjectStatus))]
        public EnumProjectStatus Status { get; set; }

        [Column("attribute", TypeName = "JSON")]
        [Description("List of attributes of the property")]
        public string[]? Attribute { get; set; }

        [Column("time_start", TypeName = "datetime")]
        public DateTime? TimeStart { get; set; }

        [Column("price_min", TypeName = "decimal(18,2)")]
        public double? PriceMin { get; set; }

        [Column("price_max", TypeName = "decimal(18,2)")]
        public double? PriceMax { get; set; }

        [Column("unit_area", TypeName = "varchar(100)")]
        public string? UnitArea { get; set; }

        [Column("total_investment", TypeName = "double")]
        public double? TotalInvestment { get; set; }

        [Column("unit_currency", TypeName = "varchar(100)")]
        [EnumDataType(typeof(EnumCurrency))]
        public EnumCurrency? UnitCurrency { get; set; }

        [Column("description", TypeName = "varchar(500)")]
        public string? Description { get; set; }

        [Column("address_id")]
        [Required]
        public Guid AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        [Description("Navigation property for the associated address")]
        public Address? Address { get; set; }

        [Column("invetor_id")]
        [Required]
        public Guid InvetorId { get; set; }

        [ForeignKey(nameof(InvetorId))]
        [Description("Navigation property for the associated Invetor")]
        public Invetor? Invetor { get; set; }

        public virtual ICollection<Property>? Properties { get; set; }
        public virtual ICollection<History>? Histories { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
    }
}