using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // register open generic repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAuthenRepository, AuthenRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IFarmRepository, FarmRepository>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductBatchRepository, ProductBatchRepository>();
            services.AddScoped<IFavoriteFarmRepository, FavoriteFarmRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IPreOrderRepository, PreOrderRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<ICareEventRepository, CareEventRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPasswordOtpRepository, PasswordOtpRepository>();

            return services;
        }
    }
}
