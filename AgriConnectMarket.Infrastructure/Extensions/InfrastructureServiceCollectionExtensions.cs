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
            services.AddHttpContextAccessor();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    sql => sql.EnableRetryOnFailure()));

            services.AddCloudinaryService(configuration);
            services.AddJwtAuthentication(configuration);

            services.AddRepositories();

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

            services.AddScoped<AuthService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<FarmService>();
            services.AddScoped<CertificateService>();
            services.AddScoped<SeasonService>();
            services.AddScoped<AddressService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProductBatchService>();
            services.AddScoped<FavoriteFarmRepository>();
            services.AddScoped<CartService>();
            services.AddScoped<OrderService>();
            services.AddScoped<EventTypeService>();
            services.AddScoped<CareEventService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();
            services.AddScoped<IBatchCodeGenerator, SqlBatchCodeGenerator>();
            services.AddScoped<IOrderCodeGenerator, OrderCodeGenerator>();
            services.AddScoped<IHashingStrategy, Sha256Hashing>();

            return services;
        }
    }
}
