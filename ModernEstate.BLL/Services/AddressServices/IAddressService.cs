using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.AddressServices
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressResponse>> GetAllAsync();
        Task<AddressResponse?> GetByIdAsync(Guid id);
        Task<AddressResponse> CreateAsync(AddressRequest request);
        Task<bool> UpdateAsync(Guid id, AddressRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<Guid> CreateAddress(AddressRequest req);
        Task<AddressResponse?> GetById(Guid id);
        Task<PageResult<AddressResponse>> GetWithParamsAsync(
            string? city,
            string? district,
            string? ward,
            int pageCurrent,
            int pageSize
        );
        Task<Address> GetOrCreateAsync(AddressRequest request);
    }
}
