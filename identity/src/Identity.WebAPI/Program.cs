using Identity.WebAPI.Extensions;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureApplicationBuilder();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();