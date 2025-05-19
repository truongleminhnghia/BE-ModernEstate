
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.NewTagRepositories
{
    public class NewTagRepository : GenericRepository<NewTag>, INewTagRepository
    {
        public NewTagRepository(ApplicationDbConext context) : base(context)
        {
        }
    }
}
