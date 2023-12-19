using Identity.Application.Users.CreateUser;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddTransient<ICreateUserService, CreateUserService>();
        
        return services;
    }
}