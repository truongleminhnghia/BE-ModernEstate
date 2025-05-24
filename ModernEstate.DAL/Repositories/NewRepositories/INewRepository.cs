
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.NewRepositories
{
    public interface INewRepository : IGenericRepository<New>
    {
        Task<New> FindByTitle(string title);
        Task<New?> GetByIdWithDetailsAsync(Guid id);
    }
}
