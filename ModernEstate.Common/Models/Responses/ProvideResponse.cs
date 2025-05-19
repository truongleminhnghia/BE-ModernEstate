using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Responses
{
    public class ProvideResponse
    {
        public Guid Id { get; set; }
        public string ProvideName { get; set; }
        public string ProvideDescription { get; set; }
        public string? ProvideImage { get; set; }
        public string ProvidePhone { get; set; }
        public string ProvideEmail { get; set; }
        public string? Website { get; set; }
        public bool IsActive { get; set; }
    }
}
