using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Unitagram.Application.Contracts.Common;
using Unitagram.Application.Contracts.Identity;
using Unitagram.Application.Models;
using Unitagram.Infrastructure.Authentication;
using Unitagram.Infrastructure.Localization;

namespace Unitagram.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddAuthentication(configuration);        
        
        services.AddLocalization(options => options.ResourcesPath = "Localization");
        services.AddSingleton<ILocalizationService, LocalizationService>();
        
        return services;
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Add JWT
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();
        
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        
        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
        
        services.AddAuthorization();
        
        services.AddHttpClient<IAccessTokenService, JwtService>((sp, client) => {

            var opt = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(opt.TokenUrl);

        });
        
        IdentityModelEventSource.ShowPII = true;
    }

}