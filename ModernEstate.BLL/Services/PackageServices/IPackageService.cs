using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.PackageServices
{
    public interface IPackageService
    {
        Task<IEnumerable<PackageResponse>> GetAllAsync();
        Task<PackageResponse?> GetByIdAsync(Guid id);
        Task<PackageResponse> CreateAsync(PackageRequest request);
        Task<bool> UpdateAsync(Guid id, PackageRequest request);
        Task<bool> DeleteAsync(Guid id);
        Task<PageResult<PackageResponse>> GetWithParamsAsync(
            EnumTypePackage? typePackage,
            int pageCurrent,
            int pageSize
        );
    }
}
