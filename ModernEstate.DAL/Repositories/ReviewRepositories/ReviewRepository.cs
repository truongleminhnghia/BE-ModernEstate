
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ReviewRepositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbConext conext) : base(conext)
        {

        }

        public async Task<Review?> FindById(Guid id)
        {
            return await _context.Reviews.Include(r => r.Account).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> FindWithParams(Guid? accountId, float? fromRating, float? toRating, string? comment)
        {
            IQueryable<Review> query = _context.Reviews.Include(r => r.Account);
            if (accountId.HasValue && accountId.Value != Guid.Empty)
            {
                query = query.Where(r => r.AccountId == accountId.Value);
            }
            if (fromRating.HasValue)
            {
                query = query.Where(r => r.Rating >= fromRating.Value);
            }
            if (toRating.HasValue)
            {
                query = query.Where(r => r.Rating <= toRating.Value);
            }
            if (!string.IsNullOrWhiteSpace(comment))
            {
                query = query.Where(r => r.Comment.Contains(comment));
            }
            query = query.OrderByDescending(r => r.CreatedAt);
            return await query.ToListAsync();
        }
    }
}