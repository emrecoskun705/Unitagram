using System.Diagnostics.CodeAnalysis;
using Identity.Application;
using Identity.Infrastructure;
using Identity.WebAPI.Configurations;

namespace Identity.WebAPI.Extensions;

[ExcludeFromCodeCoverage]
public static class WebAppBuilderExtensions
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        #region Project Dependencies

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddMemoryCache();
        
        builder.Services.Configure<ClientOptions>(builder.Configuration.GetSection(nameof(ClientOptions)));
        
        #endregion Project Dependencies
        
        return builder;
    }
}