
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.BrokerRepositories
{
    public interface IBrokerRepository : IGenericRepository<Broker>
    {
        Task<bool> AnyAsync(Func<Broker, bool> predicate);
    }
}
