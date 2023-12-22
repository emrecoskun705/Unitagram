using Identity.WebAPI.Extensions;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureApplicationBuilder();

var app = builder.Build()
    .ConfigureApplication();

app.Run();