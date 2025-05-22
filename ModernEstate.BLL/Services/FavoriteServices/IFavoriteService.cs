using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.FavoriteServices
{
    public interface IFavoriteService
    {
        Task<PageResult<FavoriteResponse>> GetAllAsync(Guid? accountId, int pageCurrent, int pageSize);
        Task<FavoriteResponse?> GetByIdAsync(Guid id);
        Task<FavoriteResponse> CreateAsync(FavoriteRequest request);
        Task<bool> UpdateAsync(Guid id, FavoriteRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
