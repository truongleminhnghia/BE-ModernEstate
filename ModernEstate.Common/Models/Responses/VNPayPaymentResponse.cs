using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Responses
{
    public class VNPayPaymentResponse
    {
        public string PaymentUrl { get; set; }
        public Guid TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
