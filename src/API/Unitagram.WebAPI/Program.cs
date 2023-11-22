using Serilog;
using Unitagram.Application;
using Unitagram.Infrastructure;
using Unitagram.Persistence;
using Unitagram.Persistence.Data;
using Unitagram.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((context,  services,  loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration) // Read configuration settings from built-in IConfiguration (appsettings.json)
        .ReadFrom.Services(services);// reads out current app's services and make them available to serilog
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
    app.UseDeveloperExceptionPage();
}

if (builder.Environment.IsDevelopment() || builder.Environment.IsProduction())
{
    app.UseSwagger(); // creates endpoints for swagger.json
    app.UseSwaggerUI(options =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
        options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "1.0");
        options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v2/swagger.json", "2.0");
    });
}

app.UseRouting();
app.UseCors("all");

app.UseAuthentication();
app.UseAuthorization();

app.Run();

/// <summary>
/// make the auto-generated Program accessible programmatically
/// </summary>
public partial class Program { } 