using Felix.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Felix.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(AppDbContext).Assembly.FullName;
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString
                ("DefaultConnection"), b => b.MigrationsAssembly(assembly)));


            return services;
        }
    }
}
