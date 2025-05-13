
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;

namespace ModernEstate.DAL.Entites
{
    [Table("category")]
    [Index(nameof(CategoryType))]
    [Index(nameof(Id), IsUnique = true)]
    public class Category : BaseEntity
    {
        [Key]
        [Column("category_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public EnumCategoryType CategoryType { get; set; }

        public ICollection<Listing>? Listings { get; set; }
    }
}