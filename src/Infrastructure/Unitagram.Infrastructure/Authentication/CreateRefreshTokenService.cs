using System.Security.Cryptography;
using Unitagram.Application.Contracts.Identity;

namespace Unitagram.Infrastructure.Authentication;

internal sealed class CreateRefreshTokenService : ICreateRefreshTokenService
{
    public string CreateRefreshToken()
    {
        // Use a larger byte array for increased entropy
        byte[] bytes = new byte[128];

        // Use RandomNumberGenerator for cryptographic randomness
        using (RandomNumberGenerator randomNumberGen = RandomNumberGenerator.Create())
        {
            randomNumberGen.GetBytes(bytes);
        }

        // Base64 encode and make URL-safe
        string base64Token = Convert.ToBase64String(bytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');

        return base64Token;
    }
}