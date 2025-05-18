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
    public class AccountBuyServiceRepository : IAccountBuyServiceRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<AccountBuyService> _dbSet;

        public AccountBuyServiceRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<AccountBuyService>();
        }

        public async Task<AccountBuyService?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<AccountBuyService>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<AccountBuyService>> FindAsync(
            Expression<Func<AccountBuyService, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(AccountBuyService entity) => await _dbSet.AddAsync(entity);

        public void Update(AccountBuyService entity) => _dbSet.Update(entity);

        public void Remove(AccountBuyService entity) => _dbSet.Remove(entity);
    }
}
