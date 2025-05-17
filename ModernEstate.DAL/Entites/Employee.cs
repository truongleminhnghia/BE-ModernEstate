
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("employee")]
    [Index(nameof(Code), Name = "IX_Employee_Code")]
    [Index(nameof(AccountId), Name = "IX_Employee_AccountId")]
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("code", TypeName = "varchar(10)")]
        public string? Code { get; set; }

        [Column("account_id")]
        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Account.Employee))]
        public Account? Account { get; set; }
    }
}