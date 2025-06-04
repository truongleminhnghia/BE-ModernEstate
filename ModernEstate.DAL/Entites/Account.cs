using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("account")]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(EnumAccountStatus))]
    [Index(nameof(RoleId))]
    public class Account : BaseEntity
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

        [Column("role_id")]
        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }

        [EnumDataType(typeof(EnumAccountStatus))]
        [Column("account_status", TypeName = "nvarchar(50)")]
        [Required]
        public EnumAccountStatus? EnumAccountStatus { get; set; }

        [Column("gender", TypeName = "nvarchar(50)")]
        [EnumDataType(typeof(EnumGender))]
        public EnumGender Gender { get; set; }

        [Column("date_of_birth", TypeName = "datetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [InverseProperty(nameof(Employee.Account))]
        public Employee? Employee { get; set; }

        [InverseProperty(nameof(Broker.Account))]
        public Broker? Broker { get; set; }

        [InverseProperty(nameof(OwnerProperty.Account))]
        public OwnerProperty? OwnerProperty { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
        public virtual ICollection<AccountBuyService>? AccountServices { get; set; }
        public virtual ICollection<New>? News { get; set; }
        public virtual ICollection<PostPackage>? PostPackages { get; set; }
        public virtual ICollection<Property>? Properties { get; set; }
    }
}
