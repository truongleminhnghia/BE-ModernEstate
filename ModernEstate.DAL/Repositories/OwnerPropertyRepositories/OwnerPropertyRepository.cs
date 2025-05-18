using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.OwnerPropertyRepositories
{
    public class OwnerPropertyRepository : IOwnerPropertyRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<OwnerProperty> _dbSet;

        public OwnerPropertyRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<OwnerProperty>();
        }

        public async Task<OwnerProperty?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<OwnerProperty>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<OwnerProperty>> FindAsync(
            Expression<Func<OwnerProperty, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(OwnerProperty entity) => await _dbSet.AddAsync(entity);

        public void Update(OwnerProperty entity) => _dbSet.Update(entity);

        public void Remove(OwnerProperty entity) => _dbSet.Remove(entity);
    }
}
