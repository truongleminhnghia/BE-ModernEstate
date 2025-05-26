namespace ModernEstate.Common.Config
{
    public class VNPayConfiguration
    {
        public string TmnCode { get; set; } = "NVR44KSO";
        public string HashSecret { get; set; } = "6B0QJONIDCXQD0XM4LCYL4A50KCXNIAM";
        public string PaymentUrl { get; set; } =
            "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public string Version { get; set; } = "2.1.0";
        public string Command { get; set; } = "pay";
        public string CurrCode { get; set; } = "VND";
        public string Locale { get; set; } = "vn";
    }
}
