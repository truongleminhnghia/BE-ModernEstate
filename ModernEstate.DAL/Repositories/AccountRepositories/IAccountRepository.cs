using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> AddAccountAsync(Account account);
        Task<Account?> GetById(string id);
        Task<Account?> GetByEmail(string email);
    }
}
