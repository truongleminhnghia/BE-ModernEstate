using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TransactionRepositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> FindTransactionsAsync(
            Guid? accountId,
            EnumTypeTransaction? typeTransaction,
            EnumStatusPayment? status,
            EnumPaymentMethod? paymentMethod
        );
    }
}
