namespace Unitagram.Application.Models;

public record AccessTokenResponse
{
    public string AccessToken { get; init; } = string.Empty;
    public DateTimeOffset ExpiresIn { get; init; }
}