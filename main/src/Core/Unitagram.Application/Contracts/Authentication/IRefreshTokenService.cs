using Unitagram.Application.Models;
using Unitagram.Domain.Shared;

namespace Unitagram.Application.Contracts.Authentication;

public interface IRefreshTokenService
{
    Task<Result<RefreshTokenResponse>> RefreshToken(string token, CancellationToken cancellationToken = default);
}