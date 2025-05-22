
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("new")]
    [Index(nameof(Slug), Name = "IX_New_Slug")]
    [Index(nameof(Title), Name = "IX_New_Title")]
    [Index(nameof(StatusNew), Name = "IX_New_StatusNew")]
    [Index(nameof(PublishDate), Name = "IX_New_PublishDate")]
    [Index(nameof(AccountId), Name = "IX_New_AccountId")]
    [Index(nameof(CategoryId), Name = "IX_New_CategoryId")]
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
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Column("account_id")]
        public Guid? AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [Column("category_id")]
        [Required]
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
        public virtual ICollection<NewTag>? NewTags { get; set; }
    }
}