using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetByEmail(string email);
        Task<Account> UpdateAccount(Account account);
        Task<bool> DeleteAccount(Account account);
        Task<IEnumerable<Account>> FindAll();
        Task<PageResult<Account>> FindByPaging(int pageCurrent, int pageSize);
        Task<IEnumerable<Account>> FindWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role);
    }
}
