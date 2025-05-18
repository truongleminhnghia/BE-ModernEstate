using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.BrokerRepositories
{
    public class BrokerRepository : IBrokerRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Broker> _dbSet;

        public BrokerRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Broker>();
        }

        public async Task<Broker?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Broker>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Broker>> FindAsync(
            Expression<Func<Broker, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Broker entity) => await _dbSet.AddAsync(entity);

        public void Update(Broker entity) => _dbSet.Update(entity);

        public void Remove(Broker entity) => _dbSet.Remove(entity);
    }
}
