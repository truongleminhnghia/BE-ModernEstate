using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class PostPackageResponse
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EnumStatus Status { get; set; }
        public Guid AccountId { get; set; }
        public Guid PackageId { get; set; }
        public Guid? PostId { get; set; }
    }
}
