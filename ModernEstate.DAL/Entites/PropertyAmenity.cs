using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.DAL.Entites
{
    public class PropertyAmenity
    {
        public Guid PropertyId { get; set; }
        public Property? Property { get; set; }

        public Guid AmenityId { get; set; }
        public Amenity? Amenity { get; set; }
    }
}
