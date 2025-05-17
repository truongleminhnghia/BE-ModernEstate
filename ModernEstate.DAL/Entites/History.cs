
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("history")]
    [Index(nameof(TypeHistory), Name = "IX_History_TypeHistory")]
    [Index(nameof(ChangeBy), Name = "IX_History_ChangeBy")]
    [Index(nameof(ChangeDate), Name = "IX_History_ChangeDate")]
    [Index(nameof(ReasonChange), Name = "IX_History_ReasonChange")]
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

        [Column("property_id")]
        [Description("ID of the property associated with the history")]
        public Guid? PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        [Description("Navigation property for the associated property")]
        public Property? Property { get; set; }

        [Column("project_id")]
        [Description("ID of the project associated with the history")]
        public Guid? ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]

        [Description("Navigation property for the associated project")]
        public Project? Project { get; set; }
    }
}