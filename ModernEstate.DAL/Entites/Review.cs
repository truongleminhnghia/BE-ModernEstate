using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernEstate.DAL.Entites
{
    [Table("review")]
    public class Review : BaseEntity
    {
        [Key]
        [Column("reivew_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("account_id")]
        [Required]
        [Description("Account id review")]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [Column("rating")]
        public float? Rating { get; set; }

        [Column("comment", TypeName = "NVARCHAR(1000)")]
        public string? Comment { get; set; }
    }
}