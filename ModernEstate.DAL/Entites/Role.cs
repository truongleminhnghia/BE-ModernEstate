
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("role")]
    [Index(nameof(RoleName), Name = "IX_Role_RoleName")]
    public class Role
    {
        [Key]
        [Column("role_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("role_name", TypeName = "varchar(50)")]
        [EnumDataType(typeof(EnumRoleName))]
        [Required]
        public EnumRoleName RoleName { get; set; }

        public ICollection<Account>? Accounts { get; set; }
    }
}