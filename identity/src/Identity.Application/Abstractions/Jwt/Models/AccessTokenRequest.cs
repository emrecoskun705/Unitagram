namespace Identity.Application.Abstractions.Jwt.Models;

public record AccessTokenRequest(string Username, string Email, bool EmailVerified, bool Active, string SessionId, List<string> UserRoles);