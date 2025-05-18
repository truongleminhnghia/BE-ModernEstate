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
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }
        IRoleRepository Roles { get; }
        IAddressRepository Addresses { get; }
        IAccountBuyServiceRepository AccountBuyServiceRepository { get; }
        IBrokerRepository Brokers { get; }
        ICategoryRepository Categories { get; }
        IContactRepository Contacts { get; }
        IEmployeeRepository Employees { get; }
        IFavoriteRepository Favorites { get; }
        IHistoryRepository Histories { get; }
        IImageRepository Images { get; }
        IInvetorRepository Invetors { get; }
        INewRepository News { get; }
        INewTagRepository NewTags { get; }
        IOwnerPropertyRepository OwnerProperties { get; }
        IPackageRepository Packages { get; }
        IPostRepository Posts { get; }
        IPostPackageRepository PostPackages { get; }
        IProjectRepository Projects { get; }
        IPropertyRepository Properties { get; }
        IProvideRepository Provides { get; }
        IServiceRepository Services { get; }
        ISupportRepository Supports { get; }
        ITagRepository Tags { get; }
        ITransactionRepository Transactions { get; }

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesWithTransactionAsync();
        int SaveChangesWithTransaction(); // Lưu thay đổi với transaction đồng bộ
    }
}
