
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernEstate.DAL.Entites
{
    [Table("tag")]
    public class Tag
    {
        [Key]
        [Column("tag_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("tag_name", TypeName = "varchar(50)")]
        [Required]
        public string? TagName { get; set; }
    }
}