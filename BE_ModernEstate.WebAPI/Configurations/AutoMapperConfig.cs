using ModernEstate.BLL.Mappers;
using ShoppEcommerce_WebApp.BLL.Mappers;

namespace BE_ModernEstate.WebAPI.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AccountMapper));
            services.AddAutoMapper(typeof(RoleMapper));
            return services;
        }
    }
}
