using Identity.Application.Abstractions.Data;
using Identity.Application.Abstractions.Jwt;
using Identity.Domain.Common;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Data.Interceptors;
using Identity.Infrastructure.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseNpgsql(connectionString);
        });
        
        #region Dapper
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        #endregion
        
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IAccessTokenService, AccessTokenService>();

        return services;
    }
}