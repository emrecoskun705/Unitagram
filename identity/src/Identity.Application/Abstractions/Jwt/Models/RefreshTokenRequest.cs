using Identity.Domain;

namespace Identity.Application.Abstractions.Jwt.Models;

public record RefreshTokenRequest(User User, string SessionId);