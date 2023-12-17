using Identity.Application.Abstractions.Jwt.Models;

namespace Identity.Application.Abstractions.Jwt;

public interface IAccessTokenService
{
    AccessTokenResponse GenerateAccessToken(AccessTokenRequest request);
}