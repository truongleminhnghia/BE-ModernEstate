
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PropertyServices
{
    public interface IPropertyService
    {
        Task<Property> Save(PropertyRequest request);
        Task<PropertyResponse?> GetById(Guid id);
        Task<PropertyResponse?> GetByCode(string code);
        Task<PageResult<PropertyResponse>> GetProperties(string? Title, double? minPrice, double? maxPrice,
                                                                EnumTypeProperty? typeProperty, float? minArea,
                                                                float? maxArea, int? numberOfBedroom,
                                                                int? numberOfBathroom, int? numberOfFloor, int? numberOfRoom,
                                                                EnumStateProperty? state, EnumStatusProperty? status,
                                                                int pageCurrent, int pageSize);
        Task<bool> Update(Guid id, UpdatePropertyRequest request);
        Task<bool> Delete(Guid id);
    }
}