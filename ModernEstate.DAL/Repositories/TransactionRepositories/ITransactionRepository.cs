using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TransactionRepositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> FindAsync(Expression<Func<Transaction, bool>> predicate);

        Task AddAsync(Transaction entity);
        void Update(Transaction entity);
        void Remove(Transaction entity);
        Task<Transaction?> GetByCodeAsync(string transactionCode);
        Task<IEnumerable<Transaction>> GetByStatusAsync(EnumStatusPayment status);
        Task<IEnumerable<Transaction>> GetByTypeAsync(EnumTypeTransaction type);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId);
    }
}
