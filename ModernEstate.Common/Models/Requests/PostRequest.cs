using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PostRequest
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string AppRovedBy { get; set; }
        public string PostBy { get; set; }
        public EnumStatePost State { get; set; }
        public EnumSourceStatus SourceStatus { get; set; }
        public string? Description { get; set; }
        public string? RejectionReason { get; set; }
        public Guid PropertyId { get; set; }
        public Guid ContactId { get; set; }
        public Guid? SupportId { get; set; }
    }
}
