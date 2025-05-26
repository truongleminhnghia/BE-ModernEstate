using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Responses
{
    public class VNPayCallbackResponse
    {
        public Guid TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
