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

namespace ModernEstate.DAL.Repositories.ServiceRepositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Service> _dbSet;

        public ServiceRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Service>();
        }

        public async Task<Service?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Service>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Service>> FindAsync(
            Expression<Func<Service, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Service entity) => await _dbSet.AddAsync(entity);

        public void Update(Service entity) => _dbSet.Update(entity);

        public void Remove(Service entity) => _dbSet.Remove(entity);

        public async Task<Service?> GetByTitleAsync(string title) =>
            await _dbSet.SingleOrDefaultAsync(s => s.Title == title);

        public async Task<IEnumerable<Service>> GetByTypeAsync(EnumTypeService type) =>
            await _dbSet.Where(s => s.TypeService == type).ToListAsync();

        public async Task<IEnumerable<Service>> GetByStatusAsync(EnumStatus status) =>
            await _dbSet.Where(s => s.Status == status).ToListAsync();

        public async Task<IEnumerable<Service>> GetByProvideIdAsync(Guid provideId) =>
            await _dbSet.Where(s => s.ProvideId == provideId).ToListAsync();
    }
}
