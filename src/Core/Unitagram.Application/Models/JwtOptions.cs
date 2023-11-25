namespace Unitagram.Application.Models;

public sealed record JwtOptions
{
    public string Key { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int ExpirationMinutes { get; init; }
    public int RefreshTokenValidityInDays { get; init; }
}