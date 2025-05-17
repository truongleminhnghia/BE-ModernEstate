
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ModernEstate.DAL.Entites
{
    [Table("image")]
    [Index(nameof(PropertyId), Name = "IX_Image_PropertyId")]
    [Index(nameof(ProjectId), Name = "IX_Image_ProjectId")]
    public class Image
    {
        [Key]
        [Column("image_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("image_url", TypeName = "varchar(500)")]
        [Required]
        public string? ImageUrl { get; set; }

        [Column("property_id")]
        public Guid? PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Property? Property { get; set; }

        [Column("project_id")]
        public Guid? ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
    }
}