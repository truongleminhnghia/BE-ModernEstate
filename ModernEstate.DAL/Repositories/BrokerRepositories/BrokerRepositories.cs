
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.BrokerRepositories
{
    public class BrokerRepository : GenericRepository<Broker>, IBrokerRepository
    {
        public BrokerRepository(ApplicationDbConext context) : base(context) { }
    }
}
