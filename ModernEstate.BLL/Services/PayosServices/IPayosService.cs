

using ModernEstate.Common.Models.Requests;
using Net.payOS.Types;

namespace ModernEstate.BLL.Services.PayosServices
{
    public interface IPayosService
    {
        Task<string> CreatePaymentAsync(PostPackageReuqest request);
        Task<bool> VerifyPaymentAsync(WebhookType type);
    }
}