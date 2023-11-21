using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Unitagram.Application.Models;

namespace Unitagram.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,  IConfiguration configuration)
    {
        services.Configure<AccessTokenSettings>(configuration.GetSection(nameof(AccessTokenSettings)));
        
        // Add JWT
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // var jwtConfig = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = "",//jwtConfig.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = "",//jwtConfig.Issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("jwtConfig.Key")),
                };
            });
        
        services.AddAuthorization();

        
        return services;
    }

}