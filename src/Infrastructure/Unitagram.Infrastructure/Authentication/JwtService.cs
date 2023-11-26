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
    
    public AccessTokenResponse CreateAccessToken(AccessTokenRequest request)
    {
        throw new NotImplementedException();
    }
}