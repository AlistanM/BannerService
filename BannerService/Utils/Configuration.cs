using BannerService.Data;
using BannerService.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BannerService.Utils
{
    public class Configuration
    {
        public static void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true);

            var config = configuration.Build();
            ConfigHelper.Init(config);

            AddServices(services, config);
            AddAuthentication(services);
        }

        private static void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ConfigHelper.Issuer,
                        ValidateAudience = true,
                        ValidAudience = ConfigHelper.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = ConfigHelper.SecurityKey,
                        ValidateLifetime = true,
                    };
                });
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<ClaimService>();
            services.AddScoped<TokenGenerationService>();
            services.AddScoped<BannerDtoService>();
        }

    }
}
