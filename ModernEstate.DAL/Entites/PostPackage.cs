
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("account_package_subscriptions")]
    public class PostPackage : BaseEntity
    {

        [Key]
        [Column("account_package_subscription_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("start_date", TypeName = "datetime")]
        [Required]
        [Description("Start date of the subscription")]
        public DateTime StartDate { get; set; }

        [Column("end_date", TypeName = "datetime")]
        // [Required]
        [Description("End date of the subscription")]
        public DateTime? EndDate { get; set; }

        [Column("status", TypeName = "varchar(100)")]
        [Required]
        [Description("Status of the subscription")]
        public EnumStatus Status { get; set; }

        [Column("total_amount")]
        // [Required]
        public double? TotalAmout { get; set; }

        [Column("currency", TypeName = "varchar(10)")]
        // [Required]
        [Description("Currency of the subscription price")]
        public EnumCurrency? Currency { get; set; } = EnumCurrency.VND;

        [Column("total_day", TypeName = "int")]
        // [Required]
        [Description("Total number of days for the subscription")]
        public int? TotalDay { get; set; }

        [Column("purchase_date", TypeName = "datetime")]
        // [Required]
        [Description("Purchase date of the subscription")]
        public DateTime? PurchaseDate { get; set; }

        [Column("expired_date", TypeName = "datetime")]
        // [Required]
        [Description("Expiration date of the subscription")]
        public DateTime? ExpiredDate { get; set; }

        [Column("account_id")]
        [Required]
        [Description("ID of the account associated with the subscription")]
        public Guid? AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [Description("Navigation property for the associated account")]
        public Account? Account { get; set; }

        [Column("package_id")]
        // [Required]
        [Description("ID of the package associated with the subscription")]
        public Guid? PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        [Description("Navigation property for the associated package")]
        public Package? Package { get; set; }

        [Column("post_id")]
        [Required]
        [Description("ID of the post associated with the subscription")]
        public Guid? PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [Description("Navigation property for the associated post")]
        public virtual Post? Post { get; set; }

        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}