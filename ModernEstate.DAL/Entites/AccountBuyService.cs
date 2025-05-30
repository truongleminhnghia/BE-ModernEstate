
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("account_service")]
    public class AccountBuyService : BaseEntity
    {
        [Key]
        [Column("account_service_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("subscription_term", TypeName = "int")]
        [Required]
        public int SubscriptionTerm { get; set; }

        [Column("unit_subscription_term", TypeName = "nvarchar(100)")]
        [Required]
        [EnumDataType(typeof(UnitSubscriptionTerm))]
        public UnitSubscriptionTerm UnitSubscriptionTerm { get; set; }

        [Column("start_date", TypeName = "datetime")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column("end_date", TypeName = "datetime")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column("total_amount", TypeName = "double")]
        [Required]
        public double TotalAmount { get; set; }

        [Column("account_id")]
        [Required]
        public Guid AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [Column("service_id")]
        [Required]
        public Guid ServiceId { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public Service? Service { get; set; }

        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}