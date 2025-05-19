using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.PostPackageServices
{
    public interface IPostPackageService
    {
        Task<IEnumerable<PostPackageResponse>> GetAllAsync();
        Task<PostPackageResponse?> GetByIdAsync(Guid id);
        Task<PostPackageResponse> CreateAsync(PostPackageRequest request);
        Task<bool> UpdateAsync(Guid id, PostPackageRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
