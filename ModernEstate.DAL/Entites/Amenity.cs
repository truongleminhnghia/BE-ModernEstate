using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.DAL.Entites
{
    public class Amenity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // VD: Hồ bơi, Gym, Thang máy

        public ICollection<PropertyAmenity>? PropertyAmenities { get; set; }
    }
}
