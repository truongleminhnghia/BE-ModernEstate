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

namespace ModernEstate.DAL.Repositories.NewRepositories
{
    public class NewRepository : INewRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<New> _dbSet;

        public NewRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<New>();
        }

        public async Task<New?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<New>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<New>> FindAsync(Expression<Func<New, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(New entity) => await _dbSet.AddAsync(entity);

        public void Update(New entity) => _dbSet.Update(entity);

        public void Remove(New entity) => _dbSet.Remove(entity);

        public async Task<New?> GetBySlugAsync(string slug) =>
            await _dbSet.SingleOrDefaultAsync(n => n.Slug == slug);

        public async Task<IEnumerable<New>> GetByStatusAsync(EnumStatusNew status) =>
            await _dbSet.Where(n => n.StatusNew == status).ToListAsync();

        public async Task<IEnumerable<New>> GetByPublishDateRangeAsync(
            DateTime from,
            DateTime to
        ) => await _dbSet.Where(n => n.PublishDate >= from && n.PublishDate <= to).ToListAsync();
    }
}
