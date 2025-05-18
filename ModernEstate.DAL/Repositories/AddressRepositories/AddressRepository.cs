using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Address> _dbSet;

        public AddressRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Address>();
        }

        public async Task<Address?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Address>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Address>> FindAsync(
            Expression<Func<Address, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Address entity) => await _dbSet.AddAsync(entity);

        public void Update(Address entity) => _dbSet.Update(entity);

        public void Remove(Address entity) => _dbSet.Remove(entity);
    }
}
