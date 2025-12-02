using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class PersistenceServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

            // Optionally register UnitOfWork if you have one:
            // services.AddScoped<IUnitOfWork, EfUnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
