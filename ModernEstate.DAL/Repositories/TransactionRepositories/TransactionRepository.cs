using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TransactionRepositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbConext context)
            : base(context) { }

        public async Task<Transaction?> FinByTransactionCode(string transactionCode)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionCode == transactionCode);
        }

        public async Task<IEnumerable<Transaction>> FindTransactionsAsync(
            Guid? accountId,
            EnumTypeTransaction? typeTransaction,
            EnumStatusPayment? status,
            EnumPaymentMethod? paymentMethod
        )
        {
            IQueryable<Transaction> q = _context.Transactions;

            if (accountId.HasValue)
                q = q.Where(t => t.AccountId == accountId.Value);

            if (typeTransaction.HasValue)
                q = q.Where(t => t.TypeTransaction == typeTransaction.Value);

            if (status.HasValue)
                q = q.Where(t => t.Status == status.Value);

            if (paymentMethod.HasValue)
                q = q.Where(t => t.PaymentMethod == paymentMethod.Value);

            return await q.OrderByDescending(t => t.CreatedAt).ToListAsync();
        }
    }
}
