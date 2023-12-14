using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Contracts.Messaging;
using Unitagram.Application.Models;
using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;

namespace Unitagram.Application.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    private readonly IAccessTokenService _accessTokenService;

    public LoginUserCommandHandler(IAccessTokenService accessTokenService) {
        _accessTokenService = accessTokenService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _accessTokenService.CreateAccessToken(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFailure) 
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        return new AccessTokenResponse(result.Value.AccessToken, result.Value.RefreshToken);
    }
}