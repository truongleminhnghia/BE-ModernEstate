
namespace ModernEstate.DAL.Entites
{
    // [NotMapped]
    public class BaseEntity
    {
        // [Column("create_at")]
        public DateTime CreatedAt { get; set; }
        // [Column("create_at")]
        public DateTime UpdatedAt { get; set; }
    }
}