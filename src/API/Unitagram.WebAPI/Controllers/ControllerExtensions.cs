using Microsoft.AspNetCore.Mvc;
using Unitagram.Application.Contracts.Common;
using Unitagram.Domain.Shared;

namespace Unitagram.WebAPI.Controllers;

public static class ControllerExtensions
{
    public static ActionResult<TResult> ToOk<TResult>(this Result<TResult> result, ILocalizationService localizationService)
    {
        if (result.IsFailure)
        {
            var errorCode = result.Error.Code; 
            
            var problemDetails = new ProblemDetails
            {
                Type = "AccountFailure",
                Extensions =
                {
                    ["error"] = new Error(errorCode, localizationService[errorCode])
                }
            };
            
            return new BadRequestObjectResult(problemDetails);
        }

        return new OkObjectResult(result.Value);
    }
}