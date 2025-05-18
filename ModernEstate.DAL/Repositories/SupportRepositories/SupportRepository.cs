using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.SupportRepositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Support> _dbSet;

        public SupportRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Support>();
        }

        public async Task<Support?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Support>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Support>> FindAsync(
            Expression<Func<Support, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Support entity) => await _dbSet.AddAsync(entity);

        public void Update(Support entity) => _dbSet.Update(entity);

        public void Remove(Support entity) => _dbSet.Remove(entity);
    }
}
