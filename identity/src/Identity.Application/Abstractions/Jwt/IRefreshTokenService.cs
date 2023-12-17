using Identity.Application.Abstractions.Jwt.Models;

namespace Identity.Application.Abstractions.Jwt;

public interface IRefreshTokenService
{
    RefreshTokenResponse GenerateRefreshToken(RefreshTokenRequest request);
}