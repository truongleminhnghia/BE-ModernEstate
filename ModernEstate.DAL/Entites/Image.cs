
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("image")]
    [Index(nameof(ImageUrl))]
    [Index(nameof(Id), IsUnique = true)]
    public class Image
    {
        [Key]
        [Column("image_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("image_url", TypeName = "varchar(500)")]
        [Required]
        public string? ImageUrl { get; set; }
    }
}