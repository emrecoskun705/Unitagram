using Unitagram.Application.Contracts.Identity;
using Unitagram.Domain.Shared;

namespace Unitagram.Persistence.Identity;

internal sealed class IdentityService : IIdentityService
{
    public Task<string?> GetUserNameAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AuthorizeAsync(Guid userId, string policyName)
    {
        throw new NotImplementedException();
    }

    public Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
