
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("transaction")]
    [Index(nameof(TransactionCode), IsUnique = true)]
    [Index(nameof(PaymentMethod))]
    [Index(nameof(Status))]
    [Index(nameof(TypeTransaction))]
    public class Transaction : BaseEntity
    {
        [Key]
        [Column("transaction_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("amount")]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public double Amount { get; set; }

        [Column("currency", TypeName = "varchar(10)")]
        [Required]
        [EnumDataType(typeof(EnumCurrency))]
        public EnumCurrency Currency { get; set; }

        [Column("type_transaction", TypeName = "varchar(100)")]
        [Required]
        [EnumDataType(typeof(EnumTypeTransaction))]
        public EnumTypeTransaction TypeTransaction { get; set; }

        [Column("status", TypeName = "varchar(100)")]
        [Required]
        [EnumDataType(typeof(EnumStatusPayment))]
        public EnumStatusPayment Status { get; set; }

        [Column("payment_method", TypeName = "varchar(150)")]
        [Required]
        [EnumDataType(typeof(EnumPaymentMethod))]
        public EnumPaymentMethod PaymentMethod { get; set; }

        [Column("transaction_code", TypeName = "varchar(150)")]
        public string? TransactionCode { get; set; }

        [Column("account_id")]
        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [Column("account_service_id")]
        public Guid? AccountServiceId { get; set; }

        [ForeignKey(nameof(AccountServiceId))]
        public AccountBuyService? AccountBuyService { get; set; }

        [Column("post_package_id")]
        public Guid? PostPackageId { get; set; }

        [ForeignKey(nameof(PostPackageId))]
        public PostPackage? PostPackage { get; set; }
    }
}