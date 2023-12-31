﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unitagram.Application.Contracts.Data;
using Unitagram.Domain.Common;
using Unitagram.Domain.Users;
using Unitagram.Persistence.Data;
using Unitagram.Persistence.Data.Interceptors;
using Unitagram.Persistence.Repositories;

namespace Unitagram.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        
        Guard.Against.NullOrEmpty(connectionString, message: "Connection string 'DefaultConnection' not found.");
        
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseNpgsql(connectionString);
        });

        #region Dapper
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        #endregion
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}