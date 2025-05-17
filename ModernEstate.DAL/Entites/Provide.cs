
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernEstate.DAL.Entites
{
    [Table("provide")]
    public class Provide : BaseEntity
    {
        [Key]
        [Column("provide_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("provide_name", TypeName = "varchar(50)")]
        [Required]
        public string? ProvideName { get; set; }

        [Column("provide_description", TypeName = "varchar(500)")]
        [Required]
        public string? ProvideDescription { get; set; }
        [Column("provide_image", TypeName = "varchar(500)")]
        public string? ProvideImage { get; set; }

        [Column("provide_phone", TypeName = "varchar(50)")]
        [Required]
        public string? ProvidePhone { get; set; }

        [Column("provide_email", TypeName = "varchar(50)")]
        [Required]
        public string? ProvideEmail { get; set; }

        [Column("website", TypeName = "varchar(150)")]
        public string? Website { get; set; }

        [Column("is_active")]
        [Required]
        public bool IsActive { get; set; } = true;
    }
}