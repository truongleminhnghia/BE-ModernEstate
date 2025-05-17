
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ModernEstate.DAL.Entites
{
    [Table("image")]
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