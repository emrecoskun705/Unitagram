using System.Text.Json.Serialization;

namespace Unitagram.Infrastructure.Authentication.Models;

public sealed class AccessToken
{
    [JsonPropertyName("access_token")]
    public string Token { get; init; } = string.Empty;
}