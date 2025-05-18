using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountServiceRepositories
{
    public class AccountServiceRepository : IAccountServiceRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<AccountService> _dbSet;

        public AccountServiceRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<AccountService>();
        }

        public async Task<AccountService?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<AccountService>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<AccountService>> FindAsync(
            Expression<Func<AccountService, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(AccountService entity) => await _dbSet.AddAsync(entity);

        public void Update(AccountService entity) => _dbSet.Update(entity);

        public void Remove(AccountService entity) => _dbSet.Remove(entity);
    }
}
