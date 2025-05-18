using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.HistoryRepositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<History> _dbSet;

        public HistoryRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<History>();
        }

        public async Task<History?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<History>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<History>> FindAsync(
            Expression<Func<History, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(History entity) => await _dbSet.AddAsync(entity);

        public void Update(History entity) => _dbSet.Update(entity);

        public void Remove(History entity) => _dbSet.Remove(entity);
    }
}
