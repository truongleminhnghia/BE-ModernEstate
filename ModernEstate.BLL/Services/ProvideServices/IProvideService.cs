using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.ProvideServices
{
    public interface IProvideService
    {
        Task<IEnumerable<ProvideResponse>> GetAllAsync();
        Task<ProvideResponse?> GetByIdAsync(Guid id);
        Task<ProvideResponse> CreateAsync(ProvideRequest request);
        Task<bool> UpdateAsync(Guid id, ProvideRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
