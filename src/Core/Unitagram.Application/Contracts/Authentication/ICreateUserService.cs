using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;

namespace Unitagram.Application.Contracts.Authentication;

public interface ICreateUserService
{
    Task<Result<string>> CreateUserAsync(User user, string password, CancellationToken cancellationToken = default);
}