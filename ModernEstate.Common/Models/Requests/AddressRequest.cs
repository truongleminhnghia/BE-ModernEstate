using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Requests
{
    public class AddressRequest
    {
        public string? HouseNumber { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? AddressDetail { get; set; }
    }
}
