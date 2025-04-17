using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountRepositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository 
    {
        public AccountRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Account> AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            return account;
        }

        public async Task<Account?> GetByEmail(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(ac => ac.Email.Equals(email));
        }

        public async Task<Account?> GetById(string id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(ac => ac.Id.Equals(id));
        }
    }
}
