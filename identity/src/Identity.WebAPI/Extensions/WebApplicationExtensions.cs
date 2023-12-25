using System.Diagnostics.CodeAnalysis;
using Identity.WebAPI.Endpoints.Users;
using Identity.WebAPI.Middlewares;

namespace Identity.WebAPI.Extensions;

[ExcludeFromCodeCoverage]
public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseMiddleware<ClientValidationMiddleware>();
        
        app.UseExceptionHandler(exceptionHandlerApp
            => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
        
        app.MapUsersEndpoints();

        return app;
    }
}