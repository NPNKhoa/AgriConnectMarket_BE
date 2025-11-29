using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class ExternalServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudinaryService(this IServiceCollection services, IConfiguration configuration)
        {
            // Example: Cloudinary options via IOptions<CloudinaryOptions>
            services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));
            services.AddSingleton<ICloudinaryAdapter, CloudinaryAdapter>(); // adapter wraps SDK and uses IOptions

            services.Configure<GHTKOptions>(configuration.GetSection("GHTK"));
            services.AddHttpClient<IShippingService, GHTKShippingService>(client =>
            {
                client.BaseAddress = new Uri(configuration["GHTK:BaseUrl"]!);
                client.Timeout = TimeSpan.FromSeconds(20);
            });

            return services;
        }

        // Other Services
        // Example: HTTP clients
        //services.AddHttpClient<IMyThirdPartyClient, MyThirdPartyClient>(client =>
        //{
        //    client.BaseAddress = new Uri(configuration["ThirdParty:BaseUrl"]);
        //    client.Timeout = TimeSpan.FromSeconds(30);
        //});
    }
}
