using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.FavoriteRepositories
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<Favorite?> FindById(Guid id);
        Task<IEnumerable<Favorite>> FindWithParams(Guid? accountId, Guid? propertyId);
    }
}
