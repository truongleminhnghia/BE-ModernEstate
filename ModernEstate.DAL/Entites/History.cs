
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("history")]
    public class History : BaseEntity
    {
        [Key]
        [Column("history_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("type_history", TypeName = "varchar(100)")]
        [Required]
        [Description("Type of history change")]
        [EnumDataType(typeof(EnumHistoryChangeType))]
        public EnumHistoryChangeType TypeHistory { get; set; }

        [Column("change_by", TypeName = "varchar(100)")]
        [Required]
        [Description("User who made the change")]
        public string? ChangeBy { get; set; }

        [Column("change_date", TypeName = "datetime")]
        [Required]
        [Description("Date of the change")]
        public DateTime ChangeDate { get; set; }

        [Column("reason_change", TypeName = "varchar(500)")]
        [Description("Reason for the change")]
        public string? ReasonChange { get; set; }
    }
}