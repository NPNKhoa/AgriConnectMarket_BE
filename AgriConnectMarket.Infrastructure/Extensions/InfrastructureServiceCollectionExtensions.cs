using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.Infrastructure.Repositories;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddPersistence<AppDbContext>(configuration);
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    sql => sql.EnableRetryOnFailure()));

            services.AddCloudinaryService(configuration);
            services.AddJwtAuthentication(configuration);

            services.AddRepositories();

            services.AddScoped<IAuthenRepository, AuthenRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            services.AddScoped<AuthService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

            return services;
        }
    }
}
