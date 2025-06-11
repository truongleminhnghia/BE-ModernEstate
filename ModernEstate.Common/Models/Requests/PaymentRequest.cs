
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PaymentRequest
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        [EnumDataType(typeof(EnumTypeTransaction))]
        public EnumTypeTransaction TypeTransaction { get; set; }

        [Required]
        [EnumDataType(typeof(EnumPaymentMethod))]
        public EnumPaymentMethod PaymentMethod { get; set; }

        [Required]
        public Guid AccountId { get; set; }
    }
}