using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Models;
using Unitagram.Domain.Shared;
using Unitagram.Infrastructure.Authentication.Models;

namespace Unitagram.Infrastructure.Authentication;

internal sealed class RefreshTokenService : IRefreshTokenService
{
    private static readonly Error RefreshTokenFailed = new(
        "Keycloak.RefreshTokenFailed",
        "Failed to acquire access token do to refresh token failure");
    
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly ILogger<RefreshTokenService> _logger;

    public RefreshTokenService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions, ILogger<RefreshTokenService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<Result<RefreshTokenResponse>> RefreshToken(string token, CancellationToken cancellationToken = default)
    {
        try
        {

            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("grant_type", "refresh_token"),
                new("refresh_token", token),
            };
            
            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken: cancellationToken);

            if (authorizationToken is null)
            {
                _logger.LogCritical("KeyCloak authorization token error; Class: {0}; Method: {1}",
                    nameof(JwtService), nameof(RefreshToken));
                return Result.Failure<RefreshTokenResponse>(RefreshTokenFailed);
            }

            return new RefreshTokenResponse(authorizationToken.AccessToken);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<RefreshTokenResponse>(RefreshTokenFailed);
        }
    }
}