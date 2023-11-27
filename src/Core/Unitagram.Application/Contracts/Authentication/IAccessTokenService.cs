using Unitagram.Application.Models;

namespace Unitagram.Application.Contracts.Authentication;

public interface IAccessTokenService
{
    AccessTokenResponse CreateAccessToken(AccessTokenRequest request);
}