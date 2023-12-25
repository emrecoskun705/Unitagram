using Identity.WebAPI.Configurations;
using Microsoft.Extensions.Options;

namespace Identity.WebAPI.Middlewares;

public sealed class ClientValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ClientOptions _clientOptions;

    public ClientValidationMiddleware(RequestDelegate next, IOptions<ClientOptions> clientOptions)
    {
        _next = next;
        _clientOptions = clientOptions.Value;
    }
    
    public async Task Invoke(HttpContext context)
    {
        string? apiKey = context.Request.Headers["ClientId"];
        string? secretId = context.Request.Headers["SecretId"];

        if (IsValidApiKey(apiKey, secretId))
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid API key or Secret ID");
        }
    }
    
    private bool IsValidApiKey(string? clientId, string? secretId)
    {
        if (_clientOptions.Id == clientId && _clientOptions.Secret == secretId)
        {
            return true;
        }

        return false;
    }
}