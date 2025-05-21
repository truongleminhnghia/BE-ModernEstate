
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PropertyRepositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<Property?> FindById(Guid id);
        Task<Property?> FindByCode(string code);
        Task<Property?> FindByTitle(string title);
        Task<IEnumerable<Property>> FindProperties(string? Title, double? minPrice, double? maxPrice,
                                                                EnumTypeProperty? typeProperty, float? minArea,
                                                                float? maxArea, int? numberOfBedroom,
                                                                int? numberOfBathroom, int? numberOfFloor, int? numberOfRoom,
                                                                EnumStateProperty? state, EnumStatusProperty? status);
    }

}
