using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Unitagram.Application.Contracts.Identity;
using Unitagram.Application.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Unitagram.Infrastructure.Authentication;

internal sealed class JwtService : IAccessTokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public AccessTokenResponse CreateAccessToken(AccessTokenRequest request)
    {
        DateTime expiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtOptions.ExpirationMinutes));

        // Create an array of Claim objects representing the user's claims, such as their ID, name, email, etc.
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
            new Claim(JwtRegisteredClaimNames.Sub, request.Username), //Subject (user id)
            new Claim(JwtRegisteredClaimNames.Email, request.Email), //Subject (user email)
            new Claim(JwtRegisteredClaimNames.Iat,
                DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)), //Issued at (date and time of token generation)
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenGenerator = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );

        // Create a JwtSecurityTokenHandler object and use it to write the token as a string.
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string token = tokenHandler.WriteToken(tokenGenerator);

        return new()
        {
            AccessToken = token,
            ExpiresIn = expiration
        };
    }
}