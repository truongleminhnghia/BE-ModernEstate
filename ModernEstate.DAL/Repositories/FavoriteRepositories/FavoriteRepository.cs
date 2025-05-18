using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.FavoriteRepositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Favorite> _dbSet;

        public FavoriteRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Favorite>();
        }

        public async Task<Favorite?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Favorite>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Favorite>> FindAsync(
            Expression<Func<Favorite, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Favorite entity) => await _dbSet.AddAsync(entity);

        public void Update(Favorite entity) => _dbSet.Update(entity);

        public void Remove(Favorite entity) => _dbSet.Remove(entity);
    }
}
