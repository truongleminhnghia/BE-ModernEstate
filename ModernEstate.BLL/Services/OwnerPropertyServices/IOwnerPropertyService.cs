
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.OwnerPropertyServices
{
    public interface IOwnerPropertyService
    {
        Task<PropertyOwnerRegisterResponse> RegisterPropertyOwner(Guid accountId);
    }
}