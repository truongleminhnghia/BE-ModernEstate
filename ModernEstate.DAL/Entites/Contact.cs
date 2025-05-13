
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("contact")]
    [Index(nameof(AccountId))]
    [Index(nameof(ListingId))]
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(AccountId), nameof(ListingId), IsUnique = true)]
    [Index(nameof(AccountId), nameof(Id), IsUnique = true)]
    [Index(nameof(ListingId), nameof(Id), IsUnique = true)]
    public class Contact : BaseEntity
    {
        [Key]
        [Column("contact_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("phone", TypeName = "varchar(20)")]
        [Required]
        public string? Phone { get; set; }

        [Column("message", TypeName = "varchar(500)")]
        public string? Message { get; set; }

        [Column("account_id")]
        [Required]
        public Guid? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }

        [Column("listing_id")]
        [Required]
        public Guid? ListingId { get; set; }

        [ForeignKey("ListingId")]
        [InverseProperty("Contacts")]
        public Listing? Listing { get; set; }
    }
}