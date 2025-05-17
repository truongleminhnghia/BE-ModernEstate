
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("post")]
    [Index(nameof(Code), IsUnique = true)]
    [Index(nameof(State))]
    [Index(nameof(SourceStatus))]
    public class Post : BaseEntity
    {
        [Key]
        [Column("post_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(150)")]
        [Required]
        public string? Code { get; set; }

        [Column("title", TypeName = "varchar(400)")]
        [Required]
        public string? Title { get; set; }

        [Column("post_by", TypeName = "varchar(400)")]
        [Required]
        public string? PostBy { get; set; }

        [Column("state", TypeName = "varchar(300)")]
        [Required]
        [Description("State of the post.")]
        [EnumDataType(typeof(EnumStatePost))]
        public EnumStatePost State { get; set; }

        [Column("source_status", TypeName = "varchar(300)")]
        [Required]
        [Description("Source status of the post.")]
        [EnumDataType(typeof(EnumSourceStatus))]
        public EnumSourceStatus SourceStatus { get; set; }

        [Column("description", TypeName = "text")]
        public string? Description { get; set; }

        [Column("rejection_reason", TypeName = "text")]
        [Description("Reason for rejection, if applicable.")]
        public string? RejectionReason { get; set; }
    }
}