using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.NewServices
{
    public interface INewService
    {
        Task<IEnumerable<NewResponse>> GetAllAsync();
        Task<NewResponse?> GetByIdAsync(Guid id);
        Task<NewResponse> CreateAsync(NewRequest request);
        Task<bool> UpdateAsync(Guid id, NewRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
