﻿namespace ModernEstate.Common.Models.Responses
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
    }
}
