
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernEstate.DAL.Entites
{
    [Table("owner_property")]
    public class OwnerProperty
    {
        [Key]
        [Column("owner_property_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(10)")]
        [Required]
        public string? Code { get; set; }
    }
}