
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("category")]
    [Index(nameof(CategoryName), Name = "IX_Category_CategoryName")]
    [Index(nameof(Id), Name = "IX_Category_Id")]
    public class Category : BaseEntity
    {
        [Key]
        [Column("category_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("category_name", TypeName = "varchar(50)")]
        [Required]
        public EnumCategoryName CategoryName { get; set; }
    }
}