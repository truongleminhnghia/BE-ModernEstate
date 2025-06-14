using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PackageRequest
    {
        // public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public double Price { get; set; }
        public EnumTypePackage TypePackage { get; set; }
        public string? Description { get; set; }
    }
}
