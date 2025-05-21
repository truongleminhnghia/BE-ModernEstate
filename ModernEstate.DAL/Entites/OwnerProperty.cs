
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("owner_property")]
    [Index(nameof(AccountId), Name = "IX_OwnerProperty_AccountId")]
    [Index(nameof(Code), Name = "IX_OwnerProperty_Code", IsUnique = true)]
    [Index(nameof(Id), Name = "IX_OwnerProperty_Id")]
    public class OwnerProperty
    {
        [Key]
        [Column("owner_property_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(30)")]
        [Required]
        public string? Code { get; set; }

        [Column("account_id")]
        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Account.OwnerProperty))]
        public Account? Account { get; set; }

        public virtual ICollection<Property>? Properties { get; set; }
    }
}