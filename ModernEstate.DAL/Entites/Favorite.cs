
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("favorite")]
    [Index(nameof(AccountId), Name = "IX_Favorite_AccountId")]
    [Index(nameof(PropertyId), Name = "IX_Favorite_PropertyId")]
    [Index(nameof(Id), Name = "IX_Favorite_Id")]
    public class Favorite : BaseEntity
    {
        [Key]
        [Column("favorite_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("account_id")]
        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [Column("property_id")]
        [Required]
        public Guid PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Property? Property { get; set; }
    }
}