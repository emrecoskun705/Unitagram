using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Application.Abstractions.Clock;
using Identity.Application.Abstractions.Jwt;
using Identity.Application.Abstractions.Jwt.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Jwt;

internal class RefreshTokenService(IOptions<RefreshTokenOptions> refreshTokenOptions,
    IDateTimeProvider dateTimeProvider) : IRefreshTokenService
{
    private readonly RefreshTokenOptions _refreshTokenOptions = refreshTokenOptions.Value;

    public RefreshTokenResponse GenerateRefreshToken(RefreshTokenRequest request)
    {
        var expiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_refreshTokenOptions.ExpirationDays));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.User.Username), //Subject (user id)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
            new Claim(JwtRegisteredClaimNames.Sid, request.SessionId), // User's session ID
            new Claim(JwtRegisteredClaimNames.Typ, "Refresh"),
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenOptions.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.RsaSha256);

        var tokenGenerator = new JwtSecurityToken(
            _refreshTokenOptions.Issuer,
            _refreshTokenOptions.Audience,
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(tokenGenerator);

        return new RefreshTokenResponse(token);
    }
}