using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Contracts.Messaging;
using Unitagram.Application.Models;
using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;

namespace Unitagram.Application.Users.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IRefreshTokenService _refreshTokenService;

    public RefreshTokenCommandHandler(IRefreshTokenService refreshTokenService)
    {
        _refreshTokenService = refreshTokenService;
    }

    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await _refreshTokenService.RefreshToken(
            request.Token,
            cancellationToken);

        if (result.IsFailure) 
        {
            return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidCredentials);
        }

        return new RefreshTokenResponse(result.Value.AccessToken);
    }
}