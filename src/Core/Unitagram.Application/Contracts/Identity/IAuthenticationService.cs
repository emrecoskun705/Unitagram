using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;

namespace Unitagram.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<Result<string>> CreateUserAsync(User user, string password, CancellationToken cancellationToken = default);
}