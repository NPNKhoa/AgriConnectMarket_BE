using AgriConnectMarket.Infrastructure.Repositories;
using AgriConnectMarket.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class UseCaseServiceCollection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
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
            services.AddScoped<ShippingFeeService>();
            services.AddScoped<VnPayService>();
            services.AddScoped<StatisticService>();
            services.AddScoped<FavoriteFarmService>();

            return services;
        }
    }
}
