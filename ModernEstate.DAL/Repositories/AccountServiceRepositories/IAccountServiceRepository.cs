using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountServiceRepositories
{
    public interface IAccountServiceRepository
    {
        Task<AccountService?> GetByIdAsync(Guid id);
        Task<IEnumerable<AccountService>> GetAllAsync();
        Task<IEnumerable<AccountService>> FindAsync(
            Expression<Func<AccountService, bool>> predicate
        );

        Task AddAsync(AccountService entity);
        void Update(AccountService entity);
        void Remove(AccountService entity);
    }
}
