using Identity.Application.Abstractions.Clock;
using Identity.Application.Abstractions.Data;
using Identity.Application.Abstractions.Jwt;
using Identity.Infrastructure.Clock;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException($"connectionString");

        services.Configure<AccessTokenOptions>(configuration.GetSection(nameof(AccessTokenOptions)));
        services.Configure<RefreshTokenOptions>(configuration.GetSection(nameof(RefreshTokenOptions)));
        
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        #region Dapper
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        #endregion

        services.AddTransient<IAccessTokenService, AccessTokenService>();

        return services;
    }
}