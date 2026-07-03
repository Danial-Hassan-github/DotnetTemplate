using DotnetTemplate.Application.Interfaces;
using DotnetTemplate.Infrastructure.Data;
using DotnetTemplate.Infrastructure.Repositories;
using DotnetTemplate.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

    }
}