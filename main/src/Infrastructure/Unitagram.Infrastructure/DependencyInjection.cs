﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Contracts.Common;
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
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer();
        
        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
        
        services.AddTransient<AdminAuthorizationDelegatingHandler>();
        
        #region HttpClients 
        services.AddHttpClient<ICreateUserService, CreateUserService>((sp, client) =>
        {
            var opt = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(opt.AdminUrl);
        }).AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();
        
        services.AddHttpClient<IAccessTokenService, JwtService>((sp, client) => {

            var opt = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(opt.TokenUrl);
        });
        
        services.AddHttpClient<IRefreshTokenService, RefreshTokenService>((sp, client) => {

            var opt = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(opt.TokenUrl);
        });
        
        services.AddHttpClient<ILogoutUserService, LogoutUserService>((sp, client) => {

            var opt = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            client.BaseAddress = new Uri(opt.UserLogoutUrl);
        });
        #endregion
        
        services.AddAuthorization();
        IdentityModelEventSource.ShowPII = true;
    }

}