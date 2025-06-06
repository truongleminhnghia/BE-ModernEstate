﻿using ModernEstate.Common.Config;
using ModernEstate.Common.Models.Requests;

namespace ModernEstate.BLL.Services.VnnPayServices
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(VNPayPaymentRequest request, VNPayConfiguration config);
        bool ValidateCallback(Dictionary<string, string> vnpayData, string hashSecret);
        string CreateSecureHash(SortedList<string, string> requestData, string hashSecret);
    }
}
