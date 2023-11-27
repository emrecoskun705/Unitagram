using System.Net.Http.Json;
using Unitagram.Application.Contracts.Authentication;
using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;
using Unitagram.Infrastructure.Authentication.Models;

namespace Unitagram.Infrastructure.Authentication;

internal sealed class CreateUserService : ICreateUserService
{
    private const string PasswordCredentialType = "password";

    private readonly HttpClient _httpClient;

    public CreateUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Result<string>> CreateUserAsync(User user, string password, CancellationToken cancellationToken)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);
        
        userRepresentationModel.Credentials = new CredentialRepresentationModel[]
        {
            new()
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType
            }
        };
        
        var response = await _httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);
        
        return ExtractIdentityIdFromLocationHeader(response);
    }
    
    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header can't be null");
        }

        var userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        var userIdentityId = locationHeader.Substring(
            userSegmentValueIndex + usersSegmentName.Length);

        return userIdentityId;
    }
}