using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.PostServices
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponse>> GetAllAsync();
        Task<PostResponse?> GetByIdAsync(Guid id);
        Task<PostResponse> CreateAsync(PostRequest request);
        Task<bool> UpdateAsync(Guid id, PostRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
