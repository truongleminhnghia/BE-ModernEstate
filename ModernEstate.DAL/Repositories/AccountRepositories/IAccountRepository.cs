using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> FindById(Guid id);
        Task<Account?> GetByEmail(string email);
        Task<Account?> FindByPhone(string phone);
        Task<Account> UpdateAccount(Account account);
        Task<bool> DeleteAccount(Account account);
        Task<IEnumerable<Account>> FindWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role, EnumGender? gender, string email);
    }
}
