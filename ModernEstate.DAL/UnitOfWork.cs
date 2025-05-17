using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Repositories.AccountRepositories;
using ModernEstate.DAL.Repositories.RoleRepositories;

namespace ModernEstate.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbConext _unitOfWorkContext;
        private IAccountRepository? _accountRepository;
        private IRoleRepository? _roleRepository;


        public UnitOfWork(ApplicationDbConext context)
        {
            _unitOfWorkContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IAccountRepository Accounts => _accountRepository ??= new AccountRepository(_unitOfWorkContext);

        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_unitOfWorkContext);

        // SaveChangesWithTransaction đồng bộ
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _unitOfWorkContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"SaveChanges failed: {ex.Message}", ex);
            }
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            var strategy = _unitOfWorkContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                // Tạo một transaction scope
                await using var transaction = await _unitOfWorkContext.Database.BeginTransactionAsync();
                try
                {
                    // Thực hiện lưu thay đổi
                    var result = await _unitOfWorkContext.SaveChangesAsync();

                    // Commit transaction
                    await transaction.CommitAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    // Rollback nếu có lỗi
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException($"Transaction failed: {ex.Message}", ex);
                }
            });
        }

        public int SaveChangesWithTransaction()
        {
            var strategy = _unitOfWorkContext.Database.CreateExecutionStrategy();
            return strategy.Execute(() =>
            {
                using var transaction = _unitOfWorkContext.Database.BeginTransaction();
                try
                {
                    var result = _unitOfWorkContext.SaveChanges();
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException($"Transaction failed: {ex.Message}", ex);
                }
            });
        }
    }
}
