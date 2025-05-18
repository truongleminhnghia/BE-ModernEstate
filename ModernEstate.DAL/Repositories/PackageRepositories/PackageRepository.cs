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

namespace ModernEstate.DAL.Repositories.PackageRepositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Package> _dbSet;

        public PackageRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Package>();
        }

        public async Task<Package?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Package>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Package>> FindAsync(
            Expression<Func<Package, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Package entity) => await _dbSet.AddAsync(entity);

        public void Update(Package entity) => _dbSet.Update(entity);

        public void Remove(Package entity) => _dbSet.Remove(entity);

        public async Task<Package?> GetByCodeAsync(string packageCode) =>
            await _dbSet.SingleOrDefaultAsync(p => p.PackageCode == packageCode);

        public async Task<IEnumerable<Package>> GetByTypeAsync(EnumTypePackage type) =>
            await _dbSet.Where(p => p.TypePackage == type).ToListAsync();

        public async Task<IEnumerable<Package>> GetByAccessPriorityAsync(
            EnumAccessPriority priority
        ) => await _dbSet.Where(p => p.AccessPriority == priority).ToListAsync();
    }
}
