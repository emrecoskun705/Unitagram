using Identity.Application.Abstractions.Data;
using Identity.Application.Abstractions.Jwt;
using Identity.Domain.Common;
using Identity.Domain.Roles;
using Identity.Domain.Users;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Data.Interceptors;
using Identity.Infrastructure.Jwt;
using Identity.Infrastructure.Repositories;
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

        _ = services.Configure<AccessTokenOptions>(configuration.GetSection(nameof(AccessTokenOptions)));
        _ = services.Configure<RefreshTokenOptions>(configuration.GetSection(nameof(RefreshTokenOptions)));
        
        _ = services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        _ = services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseNpgsql(connectionString);
        });
        
        #region Dapper
        _ = services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        #endregion
        
        _ = services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

        _ = services.AddScoped<IUserRepository, UserRepository>();
        _ = services.AddScoped<IRoleRepository, RoleRepository>();
        
        _ = services.AddTransient<IAccessTokenService, AccessTokenService>();
        
        _ = services.AddSingleton(TimeProvider.System);

        return services;
    }
}