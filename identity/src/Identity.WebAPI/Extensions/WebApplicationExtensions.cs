using System.Diagnostics.CodeAnalysis;
using Identity.WebAPI.Endpoints.Users;

namespace Identity.WebAPI.Extensions;

[ExcludeFromCodeCoverage]
public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        _ = app.UseExceptionHandler(exceptionHandlerApp
            => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
        
        _ = app.MapUsersEndpoints();

        return app;
    }
}