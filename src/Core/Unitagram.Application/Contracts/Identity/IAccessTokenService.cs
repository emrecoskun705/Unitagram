using Unitagram.Application.Models;

namespace Unitagram.Application.Contracts.Identity;

public interface IAccessTokenService
{
    AccessTokenResponse CreateAccessToken(AccessTokenRequest request);
}