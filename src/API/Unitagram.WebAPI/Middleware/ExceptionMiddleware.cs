namespace Unitagram.WebAPI.Middleware;

/// <summary>
/// Exception handling middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    /// <summary>
    /// Exception handling middleware constructor
    /// </summary>
    /// <param name="next">Next request delegate</param>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    /// <summary>
    /// catch exception and send internal server error
    /// </summary>
    /// <param name="httpContext"></param>
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