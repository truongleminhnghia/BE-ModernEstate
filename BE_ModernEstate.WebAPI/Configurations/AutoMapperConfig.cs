using ModernEstate.BLL.Mappers;
using ShoppEcommerce_WebApp.BLL.Mappers;

namespace BE_ModernEstate.WebAPI.Configurations
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(
            this IServiceCollection services
        )
        {
            services.AddAutoMapper(typeof(AccountMapper));
            services.AddAutoMapper(typeof(RoleMapper));
            services.AddAutoMapper(typeof(AddressMapper));
            services.AddAutoMapper(typeof(InvetorMapper));
            services.AddAutoMapper(typeof(ProjectMapper));
            services.AddAutoMapper(typeof(ProvideMapper));
            services.AddAutoMapper(typeof(ImageMapper));
            services.AddAutoMapper(typeof(OwnerPropertyMapper));
            services.AddAutoMapper(typeof(EmployeeMapper));
            services.AddAutoMapper(typeof(BrokerMapper));
            services.AddAutoMapper(typeof(NewsMapper));
            services.AddAutoMapper(typeof(PropertyMapper));
            services.AddAutoMapper(typeof(SupperMapper));
            services.AddAutoMapper(typeof(PostMapper));
            services.AddAutoMapper(typeof(ContactMapper));
            services.AddAutoMapper(typeof(TransactionMapper));
            services.AddAutoMapper(typeof(PostPackageMapper));
            services.AddAutoMapper(typeof(ReviewMapper));
            return services;
        }
    }
}
