

using ModernEstate.Common.Models.Requests;

namespace ModernEstate.BLL.Services.PayosServices
{
    public interface IPayosService
    {
        Task<string> CreatePaymentAsync(PostPackageReuqest request);
    }
}