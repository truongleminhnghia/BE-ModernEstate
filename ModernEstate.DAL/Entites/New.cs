
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("new")]
    public class New : BaseEntity
    {
        [Key]
        [Column("new_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("title", TypeName = "varchar(100)")]
        [Required]
        public string? Title { get; set; }

        [Column("slug", TypeName = "varchar(100)")]
        [Required]
        public string? Slug { get; set; }

        [Column("status_new", TypeName = "varchar(100)")]
        [Required]
        [EnumDataType(typeof(EnumStatusNew))]
        public EnumStatusNew StatusNew { get; set; }

        [Column("content", TypeName = "text")]
        [Required]
        public string? Content { get; set; }
        [Column("image_url", TypeName = "varchar(500)")]
        [Required]
        public string? ImageUrl { get; set; }

        [Column("publish_date", TypeName = "datetime")]
        [Required]
        public DateTime PublishDate { get; set; }
    }
}