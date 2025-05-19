
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.NewRepositories
{
    public class NewRepository : GenericRepository<New>, INewRepository
    {
        public NewRepository(ApplicationDbConext context) : base(context)
        {
        }
    }
}
