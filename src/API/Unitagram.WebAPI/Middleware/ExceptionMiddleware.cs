namespace Unitagram.WebAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
    
}