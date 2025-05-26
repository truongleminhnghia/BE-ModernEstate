using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Config;
using ModernEstate.Common.Models.Requests;

namespace ModernEstate.BLL.Services.VnnPayServices
{
    public class VNPayService : IVNPayService
    {
        public string CreatePaymentUrl(VNPayPaymentRequest request, VNPayConfiguration config)
        {
            var requestData = new SortedList<string, string>
            {
                { "vnp_Version", config.Version },
                { "vnp_Command", config.Command },
                { "vnp_TmnCode", config.TmnCode },
                { "vnp_Amount", ((long)(request.Amount * 100)).ToString() },
                { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
                { "vnp_CurrCode", config.CurrCode },
                { "vnp_IpAddr", request.IpAddress },
                { "vnp_Locale", config.Locale },
                { "vnp_OrderInfo", request.OrderInfo },
                { "vnp_OrderType", "other" },
                { "vnp_ReturnUrl", request.ReturnUrl },
                { "vnp_TxnRef", Guid.NewGuid().ToString() },
            };

            var query = string.Join(
                "&",
                requestData.Select(kv => $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}")
            );
            var secureHash = CreateSecureHash(requestData, config.HashSecret);

            return $"{config.PaymentUrl}?{query}&vnp_SecureHash={secureHash}";
        }

        public string CreateSecureHash(SortedList<string, string> requestData, string hashSecret)
        {
            var hashData = string.Join("&", requestData.Select(kv => $"{kv.Key}={kv.Value}"));
            var vnpSecureHash = HmacSHA512(hashSecret, hashData);
            return vnpSecureHash;
        }

        public bool ValidateCallback(Dictionary<string, string> vnpayData, string hashSecret)
        {
            if (!vnpayData.ContainsKey("vnp_SecureHash"))
                return false;

            var vnpSecureHash = vnpayData["vnp_SecureHash"];
            vnpayData.Remove("vnp_SecureHash");

            var sortedData = new SortedList<string, string>(vnpayData);
            var expectedHash = CreateSecureHash(sortedData, hashSecret);

            return vnpSecureHash.Equals(expectedHash, StringComparison.OrdinalIgnoreCase);
        }

        private string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashValue = hmac.ComputeHash(inputBytes);
                foreach (var thebyte in hashValue)
                {
                    hash.Append(thebyte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}
