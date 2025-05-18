using ModernEstate.BLL.HashPasswords;
using ModernEstate.BLL.JWTServices;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.BLL.Services.AuthenticateServices;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Repositories.AccountRepositories;
using ModernEstate.DAL;
using ModernEstate.DAL.Repositories.RoleRepositories;
using ModernEstate.BLL.Services.Roles;
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
using ModernEstate.DAL.Repositories.OwnerPropertyRepositories;
using ModernEstate.DAL.Repositories.PackageRepositories;
using ModernEstate.DAL.Repositories.PostRepositories;
using ModernEstate.DAL.Repositories.PostPackageRepositories;
using ModernEstate.DAL.Repositories.ProjectRepositories;
using ModernEstate.DAL.Repositories.PropertyRepositories;
using ModernEstate.DAL.Repositories.ProvideRepositories;
using ModernEstate.DAL.Repositories.ServiceRepositories;
using ModernEstate.DAL.Repositories.SupportRepositories;
using ModernEstate.DAL.Repositories.TagRepositories;
using ModernEstate.DAL.Repositories.TransactionRepositories;
using ModernEstate.DAL.Repositories.NewTagRepositories;
using ModernEstate.DAL.Repositories.AccountServiceRepositories;

namespace BE_ModernEstate.WebAPI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountBuyServiceRepository, AccountBuyServiceRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBrokerRepository, BrokerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IInvetorRepository, InvetorRepository>();
            services.AddScoped<INewRepository, NewRepository>();
            services.AddScoped<INewTagRepository, NewTagRepository>();
            services.AddScoped<IOwnerPropertyRepository, OwnerPropertyRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostPackageRepository, PostPackageRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IProvideRepository, ProvideRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISupportRepository, SupportRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
