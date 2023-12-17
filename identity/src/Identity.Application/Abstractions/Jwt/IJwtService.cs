using Identity.Application.Abstractions.Jwt.Models;

namespace Identity.Application.Abstractions.Jwt;

public interface IJwtService
{
    JwtResponse GenerateJwt(JwtRequest request);
}