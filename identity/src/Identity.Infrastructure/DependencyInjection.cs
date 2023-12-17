using Identity.Application.Abstractions.Clock;
using Identity.Application.Abstractions.Data;
using Identity.Infrastructure.Clock;
using Identity.Infrastructure.Data;
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

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        #region Dapper
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        #endregion

        return services;
    }
}