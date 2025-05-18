using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PropertyRepositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Property> _dbSet;

        public PropertyRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Property>();
        }

        public async Task<Property?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Property>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Property>> FindAsync(
            Expression<Func<Property, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Property entity) => await _dbSet.AddAsync(entity);

        public void Update(Property entity) => _dbSet.Update(entity);

        public void Remove(Property entity) => _dbSet.Remove(entity);

        public async Task<Property?> GetByCodeAsync(string code) =>
            await _dbSet.SingleOrDefaultAsync(p => p.Code == code);

        public async Task<IEnumerable<Property>> GetByStateAsync(EnumStateProperty state) =>
            await _dbSet.Where(p => p.State == state).ToListAsync();

        public async Task<IEnumerable<Property>> GetByStatusAsync(EnumStatusProperty status) =>
            await _dbSet.Where(p => p.Status == status).ToListAsync();

        public async Task<IEnumerable<Property>> GetByTransactionTypeAsync(
            EnumTypeTransaction type
        ) => await _dbSet.Where(p => p.TypeTransaction == type).ToListAsync();

        public async Task<IEnumerable<Property>> GetByPriceRangeAsync(
            decimal minPrice,
            decimal maxPrice
        ) =>
            await _dbSet
                .Where(p => p.SalePrice >= minPrice && p.SalePrice <= maxPrice)
                .ToListAsync();
    }
}
