using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.NewServices
{
    public interface INewService
    {
        Task<NewsCreateResponse> CreateAsync(NewsRequest news);
        Task<ApiResponse> UpdateAsync(Guid id, NewsRequest request);
        Task<NewsResponse> GetByIdAsync(Guid id);
        Task<EnumStatusNew> ToggleStatusAsync(Guid id);
        Task<PageResult<NewsResponse>> GetWithParamsAsync(
                                                            string? title,
                                                            EnumStatusNew? status,
                                                            EnumCategoryName? categoryName,
                                                            string? tagName,
                                                            int pageCurrent,
                                                            int pageSize
                                                        );


    }
}