using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(Guid id);
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
        Task<bool> UpdateAsync(Guid id, CategoryRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<PageResult<CategoryResponse>> GetWithParamsAsync(
            EnumCategoryName? categoryName,
            int pageCurrent,
            int pageSize
        );
    }
}
