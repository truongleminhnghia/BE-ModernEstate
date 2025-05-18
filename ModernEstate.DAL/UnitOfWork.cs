using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Repositories.AccountRepositories;
using ModernEstate.DAL.Repositories.AccountServiceRepositories;
using ModernEstate.DAL.Repositories.AddressRepositories;
using ModernEstate.DAL.Repositories.BrokerRepositories;
using ModernEstate.DAL.Repositories.CategoryRepositories;
using ModernEstate.DAL.Repositories.ContactRepositories;
using ModernEstate.DAL.Repositories.EmployeeRepositories;
using ModernEstate.DAL.Repositories.FavoriteRepositories;
using ModernEstate.DAL.Repositories.HistoryRepositories;
using ModernEstate.DAL.Repositories.ImageRepositories;
using ModernEstate.DAL.Repositories.InvetorRepositories;
using ModernEstate.DAL.Repositories.NewRepositories;
using ModernEstate.DAL.Repositories.NewTagRepositories;
using ModernEstate.DAL.Repositories.OwnerPropertyRepositories;
using ModernEstate.DAL.Repositories.PackageRepositories;
using ModernEstate.DAL.Repositories.PostPackageRepositories;
using ModernEstate.DAL.Repositories.PostRepositories;
using ModernEstate.DAL.Repositories.ProjectRepositories;
using ModernEstate.DAL.Repositories.PropertyRepositories;
using ModernEstate.DAL.Repositories.ProvideRepositories;
using ModernEstate.DAL.Repositories.RoleRepositories;
using ModernEstate.DAL.Repositories.ServiceRepositories;
using ModernEstate.DAL.Repositories.SupportRepositories;
using ModernEstate.DAL.Repositories.TagRepositories;
using ModernEstate.DAL.Repositories.TransactionRepositories;

namespace ModernEstate.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbConext _unitOfWorkContext;
        private IAccountRepository? _accountRepository;
        private IRoleRepository? _roleRepository;
        private IAddressRepository? _addressRepository;
        private IAccountBuyServiceRepository? _accountServiceRepository;
        private IBrokerRepository? _brokerRepository;
        private ICategoryRepository? _categoryRepository;
        private IContactRepository? _contactRepository;
        private IEmployeeRepository? _employeeRepository;
        private IFavoriteRepository? _favoriteRepository;
        private IHistoryRepository? _historyRepository;
        private IImageRepository? _imageRepository;
        private IInvetorRepository? _invetorRepository;
        private INewRepository? _newRepository;
        private INewTagRepository? _newTagRepository;
        private IOwnerPropertyRepository? _ownerPropertyRepository;
        private IPackageRepository? _packageRepository;
        private IPostRepository? _postRepository;
        private IPostPackageRepository? _postPackageRepository;
        private IProjectRepository? _projectRepository;
        private IPropertyRepository? _propertyRepository;
        private IProvideRepository? _provideRepository;
        private IServiceRepository? _serviceRepository;
        private ISupportRepository? _supportRepository;
        private ITagRepository? _tagRepository;
        private ITransactionRepository? _transactionRepository;

        public UnitOfWork(ApplicationDbConext context)
        {
            _unitOfWorkContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IAccountRepository Accounts =>
            _accountRepository ??= new AccountRepository(_unitOfWorkContext);

        public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_unitOfWorkContext);
        public IAddressRepository Addresses =>
            _addressRepository ??= new AddressRepository(_unitOfWorkContext);

        public IAccountBuyServiceRepository AccountBuyServiceRepository =>
            _accountServiceRepository ??= new AccountBuyServiceRepository(_unitOfWorkContext);
        public IBrokerRepository Brokers =>
            _brokerRepository ??= new BrokerRepository(_unitOfWorkContext);
        public ICategoryRepository Categories =>
            _categoryRepository ??= new CategoryRepository(_unitOfWorkContext);
        public IContactRepository Contacts =>
            _contactRepository ??= new ContactRepository(_unitOfWorkContext);

        public IEmployeeRepository Employees =>
            _employeeRepository ??= new EmployeeRepository(_unitOfWorkContext);

        public IFavoriteRepository Favorites =>
            _favoriteRepository ??= new FavoriteRepository(_unitOfWorkContext);

        public IHistoryRepository Histories =>
            _historyRepository ??= new HistoryRepository(_unitOfWorkContext);
        public IImageRepository Images =>
            _imageRepository ??= new ImageRepository(_unitOfWorkContext);

        public IInvetorRepository Invetors =>
            _invetorRepository ??= new InvetorRepository(_unitOfWorkContext);

        public INewRepository News => _newRepository ??= new NewRepository(_unitOfWorkContext);

        public INewTagRepository NewTags =>
            _newTagRepository ??= new NewTagRepository(_unitOfWorkContext);

        public IOwnerPropertyRepository OwnerProperties =>
            _ownerPropertyRepository ??= new OwnerPropertyRepository(_unitOfWorkContext);
        public IPackageRepository Packages =>
            _packageRepository ??= new PackageRepository(_unitOfWorkContext);

        public IPostRepository Posts => _postRepository ??= new PostRepository(_unitOfWorkContext);
        public IPostPackageRepository PostPackages =>
            _postPackageRepository ??= new PostPackageRepository(_unitOfWorkContext);
        public IProjectRepository Projects =>
            _projectRepository ??= new ProjectRepository(_unitOfWorkContext);
        public IPropertyRepository Properties =>
            _propertyRepository ??= new PropertyRepository(_unitOfWorkContext);
        public IProvideRepository Provides =>
            _provideRepository ??= new ProvideRepository(_unitOfWorkContext);
        public IServiceRepository Services =>
            _serviceRepository ??= new ServiceRepository(_unitOfWorkContext);
        public ISupportRepository Supports =>
            _supportRepository ??= new SupportRepository(_unitOfWorkContext);
        public ITagRepository Tags => _tagRepository ??= new TagRepository(_unitOfWorkContext);
        public ITransactionRepository Transactions =>
            _transactionRepository ??= new TransactionRepository(_unitOfWorkContext);


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
                await using var transaction =
                    await _unitOfWorkContext.Database.BeginTransactionAsync();
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
