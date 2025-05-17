
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("new_tag")]
    [Index(nameof(NewId), Name = "IX_NewTag_NewId")]
    [Index(nameof(TagId), Name = "IX_NewTag_TagId")]
    [Index(nameof(Id), Name = "IX_NewTag_Id")]
    public class NewTag
    {
        [Key]
        [Column("new_tag_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("new_id")]
        [Required]
        public Guid NewId { get; set; }

        [ForeignKey(nameof(NewId))]
        public New? New { get; set; }

        [Column("tag_id")]
        [Required]
        public Guid TagId { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag? Tag { get; set; }
    }
}