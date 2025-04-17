using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("account")]
    public class Account
    {
        [Key]
        [Column("account_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("email", TypeName = "nvarchar(300)")]
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Column("last_name", TypeName = "nvarchar(300)")]
        public string? LastName { get; set; }

        [Column("first_name", TypeName = "nvarchar(300)")]
        public string? FirstName { get; set; }

        [Column("password", TypeName = "nvarchar(100)")]
        public string? Password { get; set; }

        [Column("phone", TypeName = "nvarchar(15)")]
        public string? Phone { get; set; }

        [Column("address", TypeName = "nvarchar(300)")]
        public string? Address { get; set; }

        [Column("avatar", TypeName = "longtext")]
        public string? Avatar { get; set; }

        [EnumDataType(typeof(EnumRoleName))]
        [Column("role", TypeName = "nvarchar(50)")]
        [Required]
        public EnumRoleName? Role { get; set; }

        [EnumDataType(typeof(EnumAccountStatus))]
        [Column("account_status", TypeName = "nvarchar(50)")]
        [Required]
        public EnumAccountStatus? EnumAccountStatus { get; set; }
    }
}
