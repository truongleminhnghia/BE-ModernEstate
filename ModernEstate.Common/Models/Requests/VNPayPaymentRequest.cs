using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Requests
{
    public class VNPayPaymentRequest
    {
        public Guid AccountId { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public string OrderInfo { get; set; } = "Thanh toan don hang";
        public string ReturnUrl { get; set; }
        public string IpAddress { get; set; }
        //public Guid? AccountServiceId { get; set; }
        //public Guid? PostPackageId { get; set; }
    }
}
