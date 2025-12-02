using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.Infrastructure.Settings;
using AgriConnectMarket.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence<AppDbContext>(configuration);
            services.AddHttpContextAccessor();

            services.AddCloudinaryService(configuration);
            services.AddJwtAuthentication(configuration);

            services.Configure<QrSettings>(configuration.GetSection("QrSettings"));
            services.Configure<VerifyUrlObject>(configuration.GetSection("EmailVerifyUrl"));
            var emailVerifyUrl = configuration.GetSection("EmailVerifyUrl").Get<VerifyUrlObject>();
            services.AddSingleton(emailVerifyUrl);

            services.AddRepositories();
            services.AddUseCases();

            services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();
            services.AddScoped<IBatchCodeGenerator, SqlBatchCodeGenerator>();
            services.AddScoped<IOrderCodeGenerator, OrderCodeGenerator>();
            services.AddScoped<IHashingStrategy, Sha256Hashing>();
            services.AddScoped<IQrCodeGenerator, QrCodeGenerator>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            return services;
        }
    }
}
