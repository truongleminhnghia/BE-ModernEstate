
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernEstate.DAL.Entites
{
    [Table("location")]
    public class Location : BaseEntity
    {
        [Key]
        [Column("location_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("house_number")]
        public int? HouseNumber { get; set; }

        [Column("street", TypeName = "varchar(100)")]
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string? Street { get; set; }

        [Column("ward", TypeName = "varchar(100)")]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string? Ward { get; set; }

        [Column("district", TypeName = "varchar(100)")]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string? District { get; set; }

        [Column("city", TypeName = "varchar(100)")]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string? City { get; set; }

        [Column("country", TypeName = "varchar(100)")]
        [MaxLength(100)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string? Country { get; set; }

        [Column("address_detail", TypeName = "varchar(500)")]
        [MaxLength(500)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(500)]
        public string? AddressDetail { get; set; }

        [Column("latitude")]
        [StringLength(20)]
        [MinLength(1)]
        public float? Latitude { get; set; }

        [Column("longitude")]
        [StringLength(20)]
        [MinLength(1)]
        public float? Longitude { get; set; }
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}