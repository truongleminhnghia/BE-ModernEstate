
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("listing")]
    [Index(nameof(Title))]
    [Index(nameof(Description))]
    [Index(nameof(Status))]
    [Index(nameof(AccountId))]
    [Index(nameof(PropertyId))]
    [Index(nameof(CategoryId))]
    public class Listing : BaseEntity
    {
        [Key]
        [Column("listing_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("title", TypeName = "varchar(100)")]
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Column("description", TypeName = "varchar(500)")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [EnumDataType(typeof(EnumListing))]
        [Column("status", TypeName = "nvarchar(100)")]
        [Required]
        public EnumListing Status { get; set; }

        [Column("account_id")]
        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        [InverseProperty("Listings")]
        public Account? Account { get; set; }

        [Column("property_id")]
        [Required]
        public Guid PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        [InverseProperty("Listings")]
        public Property? Property { get; set; }

        [Column("category_id")]
        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Listings")]
        public Category? Category { get; set; }

        public ICollection<Image>? Images { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
    }
}