namespace Unitagram.Application.Models;

public sealed record AccessTokenRequest
{
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public IList<string> Roles { get; init; } = new List<string>();
}