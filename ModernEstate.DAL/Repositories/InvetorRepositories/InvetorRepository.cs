
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.InvetorRepositories
{
    public class InvetorRepository : GenericRepository<Invetor>, IInvetorRepository
    {
        public InvetorRepository(ApplicationDbConext context) : base(context)
        {

        }
    }
}
