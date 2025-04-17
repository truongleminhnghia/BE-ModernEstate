using ModernEstate.DAL.Repositories.AccountRepositories;

namespace ModernEstate.DAL
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }
        int SaveChangesWithTransaction();  // Lưu thay đổi với transaction đồng bộ
        Task<int> SaveChangesWithTransactionAsync();
    }
}
