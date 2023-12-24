using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Application.Abstractions.Jwt;
using Identity.Application.Abstractions.Jwt.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Jwt;

internal class AccessTokenService(IOptions<AccessTokenOptions> accessTokenOptions, TimeProvider dateTimeProvider) : IAccessTokenService
{
    private readonly AccessTokenOptions _accessTokenOptions = accessTokenOptions.Value;

    public AccessTokenResponse GenerateAccessToken(AccessTokenRequest request)
    {
        var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_accessTokenOptions.ExpirationMinutes));
        
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, request.Username), //Subject (user id)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
            new Claim(JwtRegisteredClaimNames.Sid, request.SessionId), // User's session ID
            // new Claim(JwtRegisteredClaimNames.Typ, JwtBearerDefaults.AuthenticationScheme),
            new Claim(JwtRegisteredClaimNames.Iat,
                dateTimeProvider.GetUtcNow().ToString(CultureInfo.InvariantCulture)), //Issued at (date and time of token generation)
            new Claim(JwtRegisteredClaimNames.Email, request.Email),
            new Claim(JwtCustomClaimNames.EmailVerified, request.EmailVerified.ToString()),
            new Claim(JwtCustomClaimNames.UserEnabled, request.Active.ToString()),
            new Claim(JwtCustomClaimNames.Roles,  Newtonsoft.Json.JsonConvert.SerializeObject(request.UserRoles))
        };
        
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenOptions.Key));
        
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        
        var tokenGenerator = new JwtSecurityToken(
           _accessTokenOptions.Issuer,
            _accessTokenOptions.Audience,
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(tokenGenerator);

        return new AccessTokenResponse(token);
    }
}