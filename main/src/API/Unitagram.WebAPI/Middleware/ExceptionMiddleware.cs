using Microsoft.AspNetCore.Mvc;
using Unitagram.Application.Exceptions;

namespace Unitagram.WebAPI.Middleware;

/// <summary>
/// Exception handling middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    
    /// <summary>
    /// Custom exception handler    
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var exceptionInformation = GetExceptionInformation(exception);

            var problemDetails = new ProblemDetails
            {
                Status = exceptionInformation.Status,
                Title = exceptionInformation.Title,
                Type = exceptionInformation.Type,
                Detail = exceptionInformation.Detail,
            };

            if (exceptionInformation.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exceptionInformation.Errors;
            }

            context.Response.StatusCode = exceptionInformation.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
    
    private static ExceptionDetails GetExceptionInformation(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation error",
                "One or more validation errors has occurred",
                validationException.Errors),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "InternalServerError",
                "Internal server error",
                "An unexpected error has occurred",
                null)
        };
    }

    private record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}