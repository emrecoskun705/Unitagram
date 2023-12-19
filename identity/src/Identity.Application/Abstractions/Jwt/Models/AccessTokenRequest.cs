using Identity.Domain.Users;

namespace Identity.Application.Abstractions.Jwt.Models;

public record AccessTokenRequest(User User, string SessionId);