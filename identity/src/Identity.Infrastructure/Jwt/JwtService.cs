using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Application.Abstractions.Clock;
using Identity.Application.Abstractions.Jwt;
using Identity.Application.Abstractions.Jwt.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Jwt;

internal class JwtService(IOptions<JwtOptions> jwtOptions, IDateTimeProvider dateTimeProvider) : IJwtService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public JwtResponse GenerateJwt(JwtRequest request)
    {
        DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOptions.ExpirationMinutes));
        
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, request.User.Username), //Subject (user id)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
            new Claim(JwtRegisteredClaimNames.Sid, request.SessionId), // User's session ID
            new Claim(JwtRegisteredClaimNames.Iat,
                dateTimeProvider.UtcNow.ToString(CultureInfo.InvariantCulture)), //Issued at (date and time of token generation)
            new Claim(JwtRegisteredClaimNames.Email, request.User.Email??""),
            new Claim(JwtCustomClaimNames.EmailVerified, request.User.EmailVerified.ToString().ToLower()),
            new Claim(JwtCustomClaimNames.UserEnabled, request.User.Active.ToString().ToLower()),
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
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string token = tokenHandler.WriteToken(tokenGenerator);

        return new JwtResponse(token);
    }
}