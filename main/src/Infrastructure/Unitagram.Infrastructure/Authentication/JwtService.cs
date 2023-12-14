using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Models;
using Unitagram.Domain.Shared;
using Unitagram.Infrastructure.Authentication.Models;

namespace Unitagram.Infrastructure.Authentication;

internal sealed class JwtService : IAccessTokenService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed",
        "Failed to acquire access token do to authentication failure");
    
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly ILogger<JwtService> _logger;

    public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions, ILogger<JwtService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _keycloakOptions = keycloakOptions.Value;
    }
    
    public async Task<Result<AccessTokenResponse>> CreateAccessToken(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password)
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken: cancellationToken);

            if (authorizationToken is null)
            {
                _logger.LogCritical("KeyCloak authorization token error; Class: {0}; Method: {1}",
                    nameof(JwtService), nameof(CreateAccessToken));
                return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
            }

            return new AccessTokenResponse(authorizationToken.AccessToken, authorizationToken.RefreshToken);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<AccessTokenResponse>(AuthenticationFailed);
        }
    }
}