

using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.BrokerServices
{
    public interface IBrokerService
    {
        Task<BrokerRegisterResponse> RegisterBroker(Guid accountId);
    }
}