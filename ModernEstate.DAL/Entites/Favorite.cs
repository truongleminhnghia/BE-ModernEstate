
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ModernEstate.DAL.Entites
{
    [Table("favority")]
    // [Index(nameof(AccountId))]
    // [Index(nameof(ListingId))]
    // [Index(nameof(PropertyId))]
    // [Index(nameof(Id), IsUnique = true)]
    // [Index(nameof(AccountId), nameof(ListingId), IsUnique = true)]
    // [Index(nameof(AccountId), nameof(PropertyId), IsUnique = true)]
    // [Index(nameof(ListingId), nameof(PropertyId), IsUnique = true)]
    public class Favorite : BaseEntity
    {
        [Key]
        [Column("favorite_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // [Column("account_id")]
        // public Guid? AccountId { get; set; }

        // [ForeignKey("AccountId")]
        // public Account? Account { get; set; }

        // [Column("listing_id")]
        // public Guid? ListingId { get; set; }

        // [ForeignKey("ListingId")]
        // [InverseProperty("Contacts")]
        // public Listing? Listing { get; set; }

        // [Column("property_id")]
        // public Guid? PropertyId { get; set; }

        // [ForeignKey("PropertyId")]
        // [InverseProperty("Favorites")]
        // public Property? Property { get; set; }
    }
}