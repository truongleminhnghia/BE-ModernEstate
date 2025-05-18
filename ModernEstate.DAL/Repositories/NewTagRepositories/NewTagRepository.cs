using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.NewTagRepositories
{
    public class NewTagRepository : INewTagRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<NewTag> _dbSet;

        public NewTagRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<NewTag>();
        }

        public async Task<NewTag?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<NewTag>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<NewTag>> FindAsync(
            Expression<Func<NewTag, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(NewTag entity) => await _dbSet.AddAsync(entity);

        public void Update(NewTag entity) => _dbSet.Update(entity);

        public void Remove(NewTag entity) => _dbSet.Remove(entity);
    }
}
