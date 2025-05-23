using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.FavoriteRepositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ApplicationDbConext context)
            : base(context) { }

        public async Task<IEnumerable<Favorite>> FindFavoritesAsync(
            Guid? accountId,
            Guid? propertyId
        )
        {
            IQueryable<Favorite> q = _context.Favorites;

            if (accountId.HasValue)
                q = q.Where(f => f.AccountId == accountId.Value);

            if (propertyId.HasValue)
                q = q.Where(f => f.PropertyId == propertyId.Value);

            return await q.OrderByDescending(f => f.CreatedAt).ToListAsync();
        }
    }
}
