using Unitagram.Application;
using Unitagram.Infrastructure;
using Unitagram.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();