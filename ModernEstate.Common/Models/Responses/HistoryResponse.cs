using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class HistoryResponse
    {
        public Guid Id { get; set; }
        public EnumHistoryChangeType TypeHistory { get; set; }
        public string? ChangeBy { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? ReasonChange { get; set; }
    }
}