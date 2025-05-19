
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.OwnerPropertyRepositories
{
    public class OwnerPropertyRepository : GenericRepository<OwnerProperty>, IOwnerPropertyRepository
    {
        public OwnerPropertyRepository(ApplicationDbConext context) : base(context)
        {
        }
    }
}
