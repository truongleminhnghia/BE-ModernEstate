using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public EnumProjectType TypeProject { get; set; }
        public int TotalBlock { get; set; }
        public string[]? BlockName { get; set; }
        public int TotalFloor { get; set; }
        public string? Title { get; set; }
        public float ProjectArea { get; set; }
        public EnumProjectStatus Status { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProvideId { get; set; }
    }
}
