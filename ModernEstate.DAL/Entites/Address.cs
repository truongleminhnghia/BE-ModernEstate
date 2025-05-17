
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("address")]
    [Index(nameof(HouseNumber), Name = "IX_Address_HouseNumber")]
    [Index(nameof(Street), Name = "IX_Address_Street")]
    [Index(nameof(Ward), Name = "IX_Address_Ward")]
    [Index(nameof(District), Name = "IX_Address_District")]
    [Index(nameof(City), Name = "IX_Address_City")]
    public class Address
    {
        [Key]
        [Column("address_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("house_number", TypeName = "varchar(100)")]
        [Description("House number of the address")]
        public string? HouseNumber { get; set; }
        [Column("street", TypeName = "varchar(100)")]
        [Description("Street name of the address")]
        [Required]
        public string? Street { get; set; }

        [Column("ward", TypeName = "varchar(100)")]
        [Description("Ward name of the address")]
        [Required]

        public string? Ward { get; set; }
        [Column("district", TypeName = "varchar(100)")]
        [Description("District name of the address")]
        [Required]
        public string? District { get; set; }

        [Column("city", TypeName = "varchar(100)")]
        [Description("City name of the address")]
        [Required]
        public string? City { get; set; }

        [Column("country", TypeName = "varchar(100)")]
        [Description("Country name of the address")]
        [Required]
        public string? Country { get; set; }

        [Column("address_detail", TypeName = "varchar(500)")]
        [Description("Detailed address information")]
        public string? AddressDetail { get; set; }
    }
}