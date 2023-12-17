using Identity.Domain;

namespace Identity.Application.Abstractions.Jwt.Models;

public record JwtRequest(User User, string SessionId);