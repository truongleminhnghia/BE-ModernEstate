
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("invetor")]
    public class Invetor : BaseEntity
    {
        [Key]
        [Column("invertor_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(10)")]
        [Required]
        public string? Code { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        [Required]
        public string? Name { get; set; }

        [Column("company_name", TypeName = "varchar(100)")]
        public string? CompanyName { get; set; }

        [Column("tax_code", TypeName = "varchar(20)")]
        public string? TaxCode { get; set; }

        [Column("phone_number", TypeName = "varchar(15)")]
        public string? PhoneNumber { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string? Email { get; set; }

        [Column("avatar", TypeName = "varchar(500)")]
        public string? Avatar { get; set; }

        [Column("invetor_type", TypeName = "varchar(50)")]
        [Required]
        [Description("Type of the investor")]
        [EnumDataType(typeof(EnumInvetorType))]
        public EnumInvetorType InvetorType { get; set; }
    }
}