using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountRepositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Account?> GetByEmail(string email)
        {
            return await _context.Accounts.Include(a => a.Role).FirstOrDefaultAsync(ac => ac.Email.Equals(email));
        }

        public async Task<IEnumerable<Account>> FindAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<PageResult<Account>> FindByPaging(int pageCurrent, int pageSize)
        {
            var accounts = await _context.Accounts
                .Skip((pageCurrent - 1) * pageSize)
               .Take(pageSize)
                .ToListAsync();
            var total = await _context.Accounts.CountAsync();
            return new PageResult<Account>(accounts, pageSize, pageCurrent, total);
        }

        public async Task<IEnumerable<Account>> FindWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role)
        {
            IQueryable<Account> query = _context.Accounts;

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(a => a.LastName.Contains(lastName));
            }
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(a => a.FirstName.Contains(firstName));
            }
            if (status.HasValue)
            {
                query = query.Where(a => a.EnumAccountStatus == status.Value);
            }
            // if (role.HasValue)
            // {
            //     query = query.Where(a => a.Role == role.Value);
            // }
            query = query.OrderByDescending(a => a.Id); // mặc định là giảm dần, tức là cái mới nhất sẽ ở trên cùng
            // Thay đổi thứ tự sắp xếp nếu cần thiết
            return await query.ToListAsync();
        }

        public Task<Account> UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            return Task.FromResult(account);
        }

        public async Task<bool> DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            return true;
        }
    }
}
