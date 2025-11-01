using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.JwtServices;
using AgriConnectMarket.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AgriConnectMarket.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("Jwt"));
            var settings = config.GetSection("Jwt").Get<JwtSettings>()!;
            var key = Encoding.UTF8.GetBytes(settings.Secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = settings.Issuer,
                ValidateAudience = true,
                ValidAudience = settings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // Register it in DI for reuse by JwtBearer and your middleware
            services.AddSingleton(validationParameters);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = validationParameters;
                });


            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITokenValidator, JwtTokenValidator>();

            return services;
        }
    }
}
