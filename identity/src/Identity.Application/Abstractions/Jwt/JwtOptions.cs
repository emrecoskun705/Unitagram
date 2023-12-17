namespace Identity.Application.Abstractions.Jwt;

public record JwtOptions
{
    public string Key { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpirationMinutes { get; init; }
}