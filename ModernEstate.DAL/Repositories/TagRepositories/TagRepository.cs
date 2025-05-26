using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TagRepositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> FindAsync(
            Expression<Func<Tag, bool>> predicate
        )
        {
            return await _context.Tags.Where(predicate).ToListAsync();
        }

        public async Task<Tag> FindByTitle(string title)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagName == title);
        }
    }
   
}
