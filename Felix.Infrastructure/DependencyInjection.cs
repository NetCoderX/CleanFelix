using Felix.Application.Interfaces.Persistence;
using Felix.Application.Interfaces.Services;
using Felix.Infrastructure.Persistence.Context;
using Felix.Infrastructure.Persistence.Repositories;
using Felix.Infrastructure.Services;
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
                options => options.UseSqlServer(configuration.GetConnectionString
                ("DefaultConnection"), b => b.MigrationsAssembly(assembly)));
             
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IOrderingQuery, OrderingQuery>();

            return services;
        }
    }
}
