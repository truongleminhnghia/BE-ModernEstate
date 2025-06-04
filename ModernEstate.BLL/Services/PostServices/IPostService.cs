using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.PostServices
{
    public interface IPostService
    {
        Task<bool> CreatePost(PostRequest request);
        Task<PostResponse?> GetById(Guid id);
        Task<PostResponse?> GetByCode(string code);
        Task<PageResult<PostResponse>> GetPosts(string? title, EnumStatePost? state, EnumSourceStatus? srcStatus, int pageCurrent, int pageSize);
        Task<bool> UpdatePost(Guid id, UpdatePostRequest request);
        Task<bool> DeletePost(Guid id);
    }
}
