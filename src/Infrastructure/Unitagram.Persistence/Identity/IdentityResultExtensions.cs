using Microsoft.AspNetCore.Identity;
using Unitagram.Domain.Shared;

namespace Unitagram.Persistence.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {

        if (result.Succeeded)
            return Result.Success();
        
        var getIdentityErrors = string.Join('|', result.Errors.Select(e => e.Description).ToList());
        return Result.Failure(new Error("Error.Identity", getIdentityErrors));
    }
}