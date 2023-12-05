using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Unitagram.Application.Contracts.Authentication;
using Unitagram.Domain.Shared;
using Unitagram.Infrastructure.Authentication.Models;

namespace Unitagram.Infrastructure.Authentication;

internal sealed class LogoutUserService : ILogoutUserService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;

    public LogoutUserService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<bool> LogoutUser(string refreshToken, CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("refresh_token", refreshToken)
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }
}