using DotnetTemplate.Application;
using DotnetTemplate.Infrastructure;

namespace DotnetTemplate.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureDI(configuration);
            services.AddApplicationDI();
            return services;
        }
    }
}