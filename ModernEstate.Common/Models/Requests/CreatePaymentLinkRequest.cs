
namespace ModernEstate.Common.Models.Requests
{
    public class CreatePaymentLinkRequest
    {
        public string productName { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string returnUrl { get; set; }
        public string cancelUrl { get; set; }
    }
}