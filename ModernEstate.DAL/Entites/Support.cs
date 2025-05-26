
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernEstate.DAL.Entites
{
    [Table("support")]
    public class Support : BaseEntity
    {
        [Key]
        [Column("support_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("support_name", TypeName = "varchar(100)")]
        [Required]
        public string? SupportName { get; set; }

        [Column("support_email", TypeName = "varchar(100)")]
        [Required]
        public string? SupportEmail { get; set; }

        [Column("message", TypeName = "text")]
        [Required]
        public string? Message { get; set; }

        [Column("property_id")]
        public Guid? PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Column("project_id")]
        public Guid? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }
    }
}