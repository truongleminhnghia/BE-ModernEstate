namespace ModernEstate.Common.Models.Requests
{
    public class CashPaymentRequest
    {
        public Guid AccountId { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string TypeTransaction { get; set; }
        public string PaymentMethod { get; set; }

        //public Guid? AccountServiceId { get; set; }
    }
}
