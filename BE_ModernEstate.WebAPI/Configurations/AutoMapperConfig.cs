using ModernEstate.BLL.Mappers;

namespace BE_ModernEstate.WebAPI.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AccountMapper).Assembly
            );
            return services;
        }
    }
}
