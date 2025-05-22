

using ModernEstate.Common.Models.Requests;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.NewServices
{
    public interface INewService
    {
        Task<bool> CreateAsync(NewsRequest news);

    }
}