
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PostPackageRepositories
{
    public class PostPackageRepository : GenericRepository<PostPackage>, IPostPackageRepository
    {
        public PostPackageRepository(ApplicationDbConext context) : base(context) { }

        public async Task<PostPackage?> FindById(Guid id)
        {
            return await _context.PostPackages
                                        .Include(pp => pp.Post)
                                        .FirstOrDefaultAsync(pp => pp.Id == id);
        }
    }

}
