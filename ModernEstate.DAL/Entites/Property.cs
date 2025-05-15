
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ModernEstate.DAL.Entites
{
    [Table("property")]
    public class Property : BaseEntity
    {
        [Key]
        [Column("property_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string? Name { get; set; }
        public string? Building { get; set; }
        public decimal? Price { get; set; }
        public string? Type { get; set; }
        public string? OwnershipStatus { get; set; }
        public ICollection<Image>? Images { get; set; }
        public decimal? ManagementFee { get; set; }
        public bool HasGym { get; set; }
        public bool HasPool { get; set; }
        public string? Description { get; set; }
        public int Floor { get; set; }
        public string? RoomNumber { get; set; }
        public float? Area { get; set; }
        public float? UsableArea { get; set; } //DT thong thuy
        public float? GrossArea { get; set; }   //DT tim tuong
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public bool Balcony { get; set; }
        public string? Furniture { get; set; }
        public string? Direction { get; set; }
        public Guid PostedById { get; set; }
        public Account? PostedBy { get; set; }
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }
        public ICollection<Listing>? Listings { get; set; }
    }
}