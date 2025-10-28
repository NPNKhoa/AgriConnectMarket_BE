using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // register open generic repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // register custom repository if any:
            // services.AddScoped<IPlantBatchRepository, PlantBatchRepository>();

            return services;
        }
    }
}
