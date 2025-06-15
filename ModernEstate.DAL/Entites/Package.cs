
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("package")]
    public class Package : BaseEntity
    {
        [Key]
        [Column("package_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("package_code", TypeName = "varchar(150)")]
        [Required]
        [Description("Unique code for the package.")]
        public string? PackageCode { get; set; }

        [Column("package_name", TypeName = "varchar(400)")]
        [Required]
        public string? PackageName { get; set; }

        [Column("price")]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Column("priority_status", TypeName = "varchar(300)")]
        [Description("Trạng thái ưu tiên của bất động sản (Ưu tiên cao, Trung bình, Thấp)")]
        [EnumDataType(typeof(EnumPriorityStatus))]
        public EnumPriorityStatus? PriorityStatus { get; set; }

        [Column("type_package", TypeName = "varchar(50)")]
        [Required]
        [Description("Type of the package.")]
        [EnumDataType(typeof(EnumTypePackage))]
        public EnumTypePackage TypePackage { get; set; }

        [Column("description", TypeName = "varchar(1000)")]
        [Description("Description of the package.")]
        public string? Description { get; set; }
    }
}