using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
    }
}