
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("package")]
    [Index(nameof(PackageCode), IsUnique = true)]
    [Index(nameof(MaxPosts))]
    [Index(nameof(Price))]
    public class Package
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

        // [Column("duration_days", TypeName = "int")]
        // [Description("Duration of the package in days.")]
        // [Required]
        // public int DurationDays { get; set; }

        [Column("max_posts", TypeName = "int")]
        [Required]
        [Description("Maximum number of posts allowed in this package.")]
        public int MaxPosts { get; set; }

        [Column("highlighted", TypeName = "bit")]
        [Required]
        [Description("Indicates if the package is highlighted.")]
        public bool Highlighted { get; set; }

        [Column("top_listing", TypeName = "bit")]
        [Required]
        [Description("Indicates if the package includes top listing.")]
        public bool TopListing { get; set; }

        // [Column("access_priority", TypeName = "varchar(50)")]
        // [Required]
        // [Description("Access priority level for the package.")]
        // [EnumDataType(typeof(EnumAccessPriority))]
        // public EnumAccessPriority AccessPriority { get; set; }

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