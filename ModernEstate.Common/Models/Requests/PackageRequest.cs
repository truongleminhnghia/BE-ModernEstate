using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Description("Trạng thái ưu tiên của bất động sản (Ưu tiên cao, Trung bình, Thấp)")]
        [EnumDataType(typeof(EnumPriorityStatus))]
        public EnumPriorityStatus PriorityStatus { get; set; }
    }
}
