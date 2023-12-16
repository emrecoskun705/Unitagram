using System.Diagnostics.CodeAnalysis;
using Identity.Infrastructure;

namespace Identity.WebAPI.Extensions;

[ExcludeFromCodeCoverage]
public static class WebAppBuilderExtensions
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        #region Project Dependencies

        builder.Services.AddInfrastructureServices(builder.Configuration);

        #endregion Project Dependencies
        
        return builder;
    }
}