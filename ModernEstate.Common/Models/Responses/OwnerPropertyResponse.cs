using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Responses
{
    public class OwnerPropertyResponse
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
    }
}