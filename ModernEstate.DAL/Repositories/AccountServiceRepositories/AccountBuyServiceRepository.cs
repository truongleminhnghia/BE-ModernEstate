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
    public class AccountBuyServiceRepository : GenericRepository<AccountBuyService>, IAccountBuyServiceRepository
    {
        public AccountBuyServiceRepository(ApplicationDbConext context) : base(context) { }

        public async Task<AccountBuyService?> GetByIdAsync(Guid id)
        {
            return await _context.AccountBuyServices.FindAsync(id);
        }

        public async Task<IEnumerable<AccountBuyService>> GetAllAsync()
        {
            return await _context.AccountBuyServices.ToListAsync();
        }

        public async Task<IEnumerable<AccountBuyService>> FindAsync(
            Expression<Func<AccountBuyService, bool>> predicate
        )
        {
            return await _context.AccountBuyServices.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(AccountBuyService entity)
        {
            await _context.AccountBuyServices.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(AccountBuyService entity)
        {
            _context.AccountBuyServices.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(AccountBuyService entity)
        {
            _context.AccountBuyServices.Remove(entity);
            _context.SaveChanges();
        }

    }

}
