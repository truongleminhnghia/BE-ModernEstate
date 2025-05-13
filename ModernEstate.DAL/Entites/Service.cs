using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernEstate.DAL.Entites
{
    [Table("service")]
    public class Service : BaseEntity
    {
        [Key]
        [Column("service_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}