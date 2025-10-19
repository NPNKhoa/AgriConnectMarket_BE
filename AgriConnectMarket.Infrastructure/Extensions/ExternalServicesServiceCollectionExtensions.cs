using AgriConnectMarket.Infrastructure.CloudinarySettings;
using AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class ExternalServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Example: Cloudinary options via IOptions<CloudinaryOptions>
            services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));
            services.AddSingleton<ICloudinaryAdapter, CloudinaryAdapter>(); // adapter wraps SDK and uses IOptions

            // Example: HTTP clients
            //services.AddHttpClient<IMyThirdPartyClient, MyThirdPartyClient>(client =>
            //{
            //    client.BaseAddress = new Uri(configuration["ThirdParty:BaseUrl"]);
            //    client.Timeout = TimeSpan.FromSeconds(30);
            //});

            return services;
        }
    }
}
