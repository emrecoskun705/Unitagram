using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using Unitagram.Application;
using Unitagram.Infrastructure;
using Unitagram.Persistence;
using Unitagram.Persistence.Data;
using Unitagram.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom
        .Configuration(context
            .Configuration) // Read configuration settings from built-in IConfiguration (appsettings.json)
        .ReadFrom.Services(services); // reads out current app's services and make them available to serilog
});

// load configuration settings
builder.Services.AddControllers(options =>
{
    //Authorization policy
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(JwtBearerDefaults
            .AuthenticationScheme) // If you do not add AuthenticationScheme you will get an error for invalid JWT tokens
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// Enable API versioning
builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader();

    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

if (builder.Environment.IsDevelopment() || builder.Environment.IsProduction())
{
    // Add Swagger
    builder.Services.AddEndpointsApiExplorer(); // generates description for all endpoints
    builder.Services.AddSwaggerGen(options =>
    {
        var asd = AppContext.BaseDirectory;
        options.IncludeXmlComments(Path.Combine(asd, "api.xml"));

        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "UnitagramV1",
            Version = "1.0"
        });

        options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "UnitagramV2",
            Version = "2.0"
        });

    }); // generates Open API specification
}

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (builder.Environment.IsProduction())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

if (builder.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
}

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(); // creates endpoints for swagger.json
    app.UseSwaggerUI(options =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "1.0");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", "2.0");
    });
}

app.UseRouting();
app.UseCors("all");

app.UseAuthentication();
app.UseAuthorization();

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("tr"),
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
});

app.MapControllers();

app.Run();

/// <summary>
/// make the auto-generated Program accessible programmatically
/// </summary>
public partial class Program
{
}