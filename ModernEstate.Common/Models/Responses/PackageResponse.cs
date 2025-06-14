using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class PackageResponse
    {
        public Guid Id { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public double Price { get; set; }
        public EnumTypePackage TypePackage { get; set; }
        public string? Description { get; set; }
    }
}
