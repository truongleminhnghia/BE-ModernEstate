using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class ServiceRequest
    {
        public string? Title { get; set; }
        public double Price { get; set; }
        public int DurationDay { get; set; }
        public bool? IsSystem { get; set; }
        public EnumTypeService TypeService { get; set; }
        public EnumStatus Status { get; set; }
        public Guid? ProvideId { get; set; }
    }
}
