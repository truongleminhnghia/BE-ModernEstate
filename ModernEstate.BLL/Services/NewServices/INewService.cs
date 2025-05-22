

using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.NewServices
{
    public interface INewService
    {
        Task<NewsCreateResponse> CreateAsync(NewsRequest news);
        Task<bool> UpdateTitle(string name, Guid id);

    }
}