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

namespace ModernEstate.DAL.Repositories.PostRepositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Post> _dbSet;

        public PostRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Post>();
        }

        public async Task<Post?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Post>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Post>> FindAsync(Expression<Func<Post, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Post entity) => await _dbSet.AddAsync(entity);

        public void Update(Post entity) => _dbSet.Update(entity);

        public void Remove(Post entity) => _dbSet.Remove(entity);

        public async Task<Post?> GetByCodeAsync(string code) =>
            await _dbSet.SingleOrDefaultAsync(p => p.Code == code);

        public async Task<IEnumerable<Post>> GetByStateAsync(EnumStatePost state) =>
            await _dbSet.Where(p => p.State == state).ToListAsync();

        public async Task<IEnumerable<Post>> GetBySourceStatusAsync(
            EnumSourceStatus sourceStatus
        ) => await _dbSet.Where(p => p.SourceStatus == sourceStatus).ToListAsync();

        public async Task<IEnumerable<Post>> GetByPropertyIdAsync(Guid propertyId) =>
            await _dbSet.Where(p => p.PropertyId == propertyId).ToListAsync();
    }
}
