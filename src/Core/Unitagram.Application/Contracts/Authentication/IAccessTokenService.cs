using Unitagram.Application.Models;
using Unitagram.Domain.Shared;

namespace Unitagram.Application.Contracts.Authentication;

public interface IAccessTokenService
{
    Task<Result<AccessTokenResponse>> CreateAccessToken(string email, string password, CancellationToken cancellationToken = default);
}