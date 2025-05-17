using ModernEstate.DAL.Repositories.AccountRepositories;
using ModernEstate.DAL.Repositories.RoleRepositories;

namespace ModernEstate.DAL
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }
        IRoleRepository Roles { get; }

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesWithTransactionAsync();
        int SaveChangesWithTransaction();  // Lưu thay đổi với transaction đồng bộ
    }
}
