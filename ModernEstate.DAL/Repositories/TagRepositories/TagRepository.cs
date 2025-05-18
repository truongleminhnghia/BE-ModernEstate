using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TagRepositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Tag> _dbSet;

        public TagRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Tag>();
        }

        public async Task<Tag?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Tag>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Tag>> FindAsync(Expression<Func<Tag, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Tag entity) => await _dbSet.AddAsync(entity);

        public void Update(Tag entity) => _dbSet.Update(entity);

        public void Remove(Tag entity) => _dbSet.Remove(entity);

        public async Task<Tag?> GetByNameAsync(string tagName) =>
            await _dbSet.SingleOrDefaultAsync(t => t.TagName == tagName);
    }
}
