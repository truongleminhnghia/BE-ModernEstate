using Microsoft.OpenApi.Models;

namespace BE_ModernEstate.WebAPI.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerDependencies(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Định nghĩa Swagger API documentation
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Modern Estate API",
                    Description = "API for Modern Estate",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Nghĩa ngu",
                        Email = "adronghia@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // Định nghĩa Bearer token security scheme
                // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     Name = "Authorization",
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer",
                //     BearerFormat = "JWT",
                //     In = ParameterLocation.Header,
                //     Description = "JWT Authorization header using the Bearer scheme."
                // });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                // c.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             }
                //         },
                //         new string[] { }
                //     }
                // });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }});

                // Bao gồm XML comment cho Swagger
                // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ShoppEcommerceASP.Web.xml"));
            });
            return services;
        }
    }
}
