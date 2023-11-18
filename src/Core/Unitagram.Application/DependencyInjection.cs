using Microsoft.Extensions.DependencyInjection;

namespace Unitagram.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var a = "as";
        Guard.Against.NullOrEmpty(a);
        
        return services;
    }
}