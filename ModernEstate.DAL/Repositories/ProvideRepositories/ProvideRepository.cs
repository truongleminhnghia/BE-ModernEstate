using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ProvideRepositories
{
    public class ProvideRepository : IProvideRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Provide> _dbSet;

        public ProvideRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Provide>();
        }

        public async Task<Provide?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Provide>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Provide>> FindAsync(
            Expression<Func<Provide, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Provide entity) => await _dbSet.AddAsync(entity);

        public void Update(Provide entity) => _dbSet.Update(entity);

        public void Remove(Provide entity) => _dbSet.Remove(entity);
    }
}
