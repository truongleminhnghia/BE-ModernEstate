
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModernEstate.DAL.Entites
{
    [Table("contact")]
    [Index(nameof(ContactEmail), Name = "IX_Contact_ContactEmail")]
    [Index(nameof(ContactPhone), Name = "IX_Contact_ContactPhone")]
    [Index(nameof(ContactName), Name = "IX_Contact_ContactName")]
    public class Contact
    {
        [Key]
        [Column("contact_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("contact_name", TypeName = "varchar(100)")]
        [Required]
        public string? ContactName { get; set; }

        [Column("contact_email", TypeName = "varchar(100)")]
        public string? ContactEmail { get; set; }

        [Column("contact_phone", TypeName = "varchar(15)")]
        [Required]
        public string? ContactPhone { get; set; }
    }
}