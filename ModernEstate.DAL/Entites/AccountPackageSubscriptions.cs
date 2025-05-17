
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("account_package_subscriptions")]
    public class AccountPackageSubscriptions : BaseEntity
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
        [Required]
        [Description("End date of the subscription")]
        public DateTime EndDate { get; set; }

        [Column("status", TypeName = "varchar(100)")]
        [Required]
        [Description("Status of the subscription")]
        public EnumStatus Status { get; set; }
    }
}