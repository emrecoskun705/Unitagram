using Unitagram.Application;
using Unitagram.Infrastructure;
using Unitagram.Persistence;
using Unitagram.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // await app.InitialiseDatabaseAsync();
}

app.MapGet("/", () => "Hello World!");

app.Run();