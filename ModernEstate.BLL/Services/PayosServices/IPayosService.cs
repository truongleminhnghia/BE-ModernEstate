

using ModernEstate.Common.Models.Requests;
using Net.payOS.Types;

namespace ModernEstate.BLL.Services.PayosServices
{
    public interface IPayosService
    {
        Task<string> CreatePaymentAsync(Guid id);
        Task<bool> VerifyPaymentAsync(WebhookType type);
    }
}