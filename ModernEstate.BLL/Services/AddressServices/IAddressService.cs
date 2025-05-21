

using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.AddressServices
{
    public interface IAddressService
    {
        Task<Guid> CreateAddress(AddressRequest req);
        Task<AddressResponse?> GetById(Guid id);
    }
}