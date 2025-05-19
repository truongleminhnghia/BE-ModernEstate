using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountServiceRepositories
{
    public interface IAccountBuyServiceRepository : IGenericRepository<AccountBuyService>
    {
        Task<AccountBuyService?> GetByIdAsync(Guid id);
        Task<IEnumerable<AccountBuyService>> GetAllAsync();
        Task<IEnumerable<AccountBuyService>> FindAsync(
            Expression<Func<AccountBuyService, bool>> predicate
        );

        Task AddAsync(AccountBuyService entity);
        void Update(AccountBuyService entity);
        void Remove(AccountBuyService entity);
    }
  
}
