using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.Infrastructure.Repositories;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.Infrastructure.Settings;
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

            services.Configure<QrSettings>(configuration.GetSection("QrSettings"));

            services.AddRepositories();

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
            services.AddScoped<IQrCodeGenerator, QrCodeGenerator>();

            return services;
        }
    }
}
