
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PostPackageServices
{
    public interface IPostPackageService
    {
        Task<PostPackage> CreateAsync(PostPackageReuqest reuqest, Guid id);
        Task<PostPackageResponse?> GetByIdAsync(Guid id);
    }
}
