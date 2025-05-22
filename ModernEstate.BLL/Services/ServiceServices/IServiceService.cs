using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.ServiceServices
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceResponse>> GetAllAsync();
        Task<ServiceResponse?> GetByIdAsync(Guid id);
        Task<ServiceResponse> CreateAsync(ServiceRequest request);
        Task<bool> UpdateAsync(Guid id, ServiceRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
