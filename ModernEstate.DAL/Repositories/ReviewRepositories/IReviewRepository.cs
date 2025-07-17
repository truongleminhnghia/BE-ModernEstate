
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ReviewRepositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<Review?> FindById(Guid id);
        Task<IEnumerable<Review>> FindWithParams(Guid? accountId, float? fromRating, float? toRating, string? commnet);
        Task<IEnumerable<Review>> FindAll();
    }
}