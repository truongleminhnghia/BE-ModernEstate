using ModernEstate.Common.Models.AuthenticateResponse;

namespace ModernEstate.Common.Models.Responses
{
    public class CashPaymentResponse
    {
        public Guid TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string EnumTypeTransaction { get; set; }
        public string EnumPaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AccountResponse Account { get; set; }
    }
}
