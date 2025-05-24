
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

        public async Task<Favorite?> FindById(Guid id)
        {
            return await _context.Favorites.Include(a => a.Property)
                                            .FirstOrDefaultAsync(ac => ac.Id.Equals(id));
        }

        public async Task<IEnumerable<Favorite>> FindWithParams(Guid? accountId, Guid? propertyId)
        {
            IQueryable<Favorite> query = _context.Favorites.Include(a => a.Property);

            if (!string.IsNullOrWhiteSpace(accountId.ToString()))
            {
                query = query.Where(a => a.AccountId == accountId.Value);
            }
            if (!string.IsNullOrWhiteSpace(propertyId.ToString()))
            {
                query = query.Where(a => a.PropertyId == propertyId.Value);
            }
            query = query.OrderByDescending(a => a.CreatedAt); // mặc định là giảm dần, tức là cái mới nhất sẽ ở trên cùng
            // Thay đổi thứ tự sắp xếp nếu cần thiết
            return await query.ToListAsync();
        }
    }
}
