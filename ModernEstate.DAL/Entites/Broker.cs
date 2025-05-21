
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("broker")]
    [Index(nameof(Code), Name = "IX_Broker_Code", IsUnique = true)]
    [Index(nameof(AccountId), Name = "IX_Broker_AccountId")]
    public class Broker : BaseEntity
    {
        [Key]
        [Column("broker_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(30)")]
        [Required]
        public string? Code { get; set; }

        [Column("account_id")]
        [Required]
        public Guid AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Account.Broker))]
        public Account? Account { get; set; }
    }
}