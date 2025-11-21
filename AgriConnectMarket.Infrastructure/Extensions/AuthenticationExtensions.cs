using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Infrastructure.JwtServices;
using AgriConnectMarket.Infrastructure.Services;
using AgriConnectMarket.SharedKernel.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var services = context.HttpContext.RequestServices;
                            var uow = services.GetService<IUnitOfWork>();            // injectable
                            var currentUser = services.GetService<ICurrentUserService>();

                            // get user id (use ClaimTypes.NameIdentifier which .NET maps from 'sub')
                            var userIdStr = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier)
                                             ?? context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

                            if (!Guid.TryParse(userIdStr, out var userId))
                            {
                                context.Fail("Invalid user id in token");
                                return;
                            }

                            // quick DB check - active, not revoked etc.
                            var user = await uow.AuthenRepository.GetByIdAsync(userId);
                            if (user == null || !user.IsActive)
                            {
                                context.Fail("User unavailable");
                                return;
                            }

                            // Optionally refresh roles from DB if roles change frequently:
                            var role = await uow.AuthenRepository.GetRolesAsync(userId); // implement it
                            if (role != null)
                            {
                                var identity = context.Principal!.Identity as ClaimsIdentity;
                                // remove existing role claims
                                foreach (var c in identity.FindAll(ClaimTypes.Role).ToList())
                                    identity.RemoveClaim(c);

                                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                            }

                            // Set current user service
                            currentUser?.SetClaims(context.Principal);
                        },

                        OnAuthenticationFailed = context =>
                        {
                            // optional logging
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(ROLE.ADMIN));
            });

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITokenValidator, JwtTokenValidator>();

            return services;
        }
    }
}
