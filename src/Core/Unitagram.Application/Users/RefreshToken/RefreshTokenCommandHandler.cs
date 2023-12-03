using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Contracts.Messaging;
using Unitagram.Application.Models;
using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;

namespace Unitagram.Application.Users.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, AccessTokenResponse>
{
    private readonly IRefreshTokenService _refreshTokenService;

    public RefreshTokenCommandHandler(IRefreshTokenService refreshTokenService)
    {
        _refreshTokenService = refreshTokenService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await _refreshTokenService.RefreshToken(
            request.Token,
            cancellationToken);

        if (result.IsFailure) 
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        return new AccessTokenResponse(result.Value.AccessToken, result.Value.RefreshToken);
    }
}