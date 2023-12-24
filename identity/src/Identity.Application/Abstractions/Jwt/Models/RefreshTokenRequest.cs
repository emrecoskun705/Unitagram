using Identity.Domain;
using Identity.Domain.Users;

namespace Identity.Application.Abstractions.Jwt.Models;

public record RefreshTokenRequest(string IdentityId, string SessionId);