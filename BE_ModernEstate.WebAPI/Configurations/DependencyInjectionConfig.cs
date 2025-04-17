using ModernEstate.BLL.HashPasswords;
using ModernEstate.BLL.JWTServices;
using ModernEstate.BLL.Mappers;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.BLL.Services.AuthenticateServices;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Repositories.AccountRepositories;
using ModernEstate.DAL;

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

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddAutoMapper(typeof(AccountMapper));
            return services;
        }
    }
}
