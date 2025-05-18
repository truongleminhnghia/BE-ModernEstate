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

namespace ModernEstate.DAL.Repositories.PostPackageRepositories
{
    public class PostPackageRepository : IPostPackageRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<PostPackage> _dbSet;

        public PostPackageRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<PostPackage>();
        }

        public async Task<PostPackage?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<PostPackage>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<PostPackage>> FindAsync(
            Expression<Func<PostPackage, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(PostPackage entity) => await _dbSet.AddAsync(entity);

        public void Update(PostPackage entity) => _dbSet.Update(entity);

        public void Remove(PostPackage entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<PostPackage>> GetByAccountIdAsync(Guid accountId) =>
            await _dbSet.Where(x => x.AccountId == accountId).ToListAsync();

        public async Task<IEnumerable<PostPackage>> GetByPackageIdAsync(Guid packageId) =>
            await _dbSet.Where(x => x.PackageId == packageId).ToListAsync();

        public async Task<IEnumerable<PostPackage>> GetByPostIdAsync(Guid postId) =>
            await _dbSet.Where(x => x.PostId == postId).ToListAsync();

        public async Task<IEnumerable<PostPackage>> GetByStatusAsync(EnumStatus status) =>
            await _dbSet.Where(x => x.Status == status).ToListAsync();

        public async Task<IEnumerable<PostPackage>> GetByDateRangeAsync(
            DateTime from,
            DateTime to
        ) => await _dbSet.Where(x => x.StartDate >= from && x.EndDate <= to).ToListAsync();
    }
}
