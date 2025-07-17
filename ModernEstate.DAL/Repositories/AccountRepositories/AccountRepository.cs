using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
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
            return await _context.Accounts.Include(a => a.Role)
                                            .Include(a => a.Employee)
                                            .Include(a => a.Broker)
                                            .Include(a => a.OwnerProperty)
                                            .FirstOrDefaultAsync(ac => ac.Email.Equals(email));
        }

        public async Task<Account?> FindById(Guid id)
        {
            return await _context.Accounts.Include(a => a.Role)
                                            .Include(a => a.Employee)
                                            .Include(a => a.Broker)
                                            .Include(a => a.OwnerProperty)
                                            .FirstOrDefaultAsync(ac => ac.Id.Equals(id));
        }

        public async Task<IEnumerable<Account>> FindWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role, EnumGender? gender,
                                                                string email, int? limit, DateTime? fromDate, DateTime? toDate)
        {
            IQueryable<Account> query = _context.Accounts.Include(a => a.Employee).Include(a => a.OwnerProperty).Include(a => a.Broker).Include(a => a.Role);

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(a => a.LastName.Contains(lastName));
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(a => a.Email.Contains(email));
            }
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(a => a.FirstName.Contains(firstName));
            }
            if (status.HasValue)
            {
                query = query.Where(a => a.EnumAccountStatus == status.Value);
            }
            if (role.HasValue)
            {
                query = query.Where(a => a.Role.RoleName == role.Value);
            }
            if (gender.HasValue)
            {
                query = query.Where(a => a.Gender == gender.Value);
            }
            if (fromDate.HasValue)
                query = query.Where(a => a.CreatedAt >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(a => a.CreatedAt <= toDate.Value);
            query = query.OrderByDescending(a => a.CreatedAt); // mặc định là giảm dần, tức là cái mới nhất sẽ ở trên cùng
                                                               // Thay đổi thứ tự sắp xếp nếu cần thiết
            if (limit.HasValue && limit.Value > 0)
                query = query.Take(limit.Value);
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

        public async Task<Account?> FindByPhone(string phone)
        {
            return await _context.Accounts.Include(a => a.Role)
                                            .Include(a => a.Employee)
                                            .Include(a => a.Broker)
                                            .Include(a => a.OwnerProperty)
                                            .FirstOrDefaultAsync(ac => ac.Phone.Equals(phone));
        }

        public async Task<Account> GetByResetTokenAsync(String token)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.PasswordResetToken == token);
        }

        public async Task<IEnumerable<Account?>> FindAll()
        {
            return await _context.Accounts.Include(a => a.Role)
                                            .Include(a => a.Employee)
                                            .Include(a => a.Broker)
                                            .Include(a => a.OwnerProperty)
                                            .ToListAsync();
        }
    }
}
