using Identity.WebAPI.Extensions;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureApplicationBuilder();

var app = builder.Build()
    .ConfigureApplication();

app.MapGet("/ex", context => throw new ArgumentNullException("ersdfsd"));

app.Run();