
using Microsoft.EntityFrameworkCore;
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
        public async Task<New> FindByTitle(string title)
        {
            return await _context.News.FirstOrDefaultAsync(t => t.Title == title);
        }
    }
}
