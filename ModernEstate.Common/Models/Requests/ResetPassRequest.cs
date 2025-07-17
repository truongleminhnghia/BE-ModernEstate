using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Requests
{
    public class ResetPassRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
