

using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.NewServices
{
    public interface INewService
    {
        Task<NewsCreateResponse> CreateAsync(NewsRequest news);
        Task<ApiResponse> UpdateAsync(Guid id, NewsRequest request);
        Task<NewsResponse> GetByIdAsync(Guid id);


    }
}