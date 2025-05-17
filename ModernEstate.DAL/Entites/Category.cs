
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("category")]
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