using ModernEstate.DAL.Context;
using ModernEstate.DAL.Repositories.AccountRepositories;

namespace ModernEstate.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbConext _unitOfWorkContext;
        private IAccountRepository? _accountRepository;

        public UnitOfWork(ApplicationDbConext context)
        {
            _unitOfWorkContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IAccountRepository Accounts => _accountRepository ??= new AccountRepository(_unitOfWorkContext);

        // SaveChangesWithTransaction đồng bộ
        public int SaveChangesWithTransaction()
        {
            int result = -1;
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit(); // Commit nếu không lỗi
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback(); // Rollback nếu có lỗi
                    // Log exception or handle as per requirement
                    throw new InvalidOperationException("An error occurred during the transaction.", ex);
                }
            }
            return result;
        }

        // SaveChangesWithTransaction bất đồng bộ
        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;
            using (var dbContextTransaction = await _unitOfWorkContext.Database.BeginTransactionAsync())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    // Log thông tin chi tiết của lỗi
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                    throw new InvalidOperationException("An error occurred during the transaction.", ex);
                }
            }
            return result;
        }
    }
}
