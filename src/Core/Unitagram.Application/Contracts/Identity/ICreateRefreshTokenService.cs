using Unitagram.Application.Models;

namespace Unitagram.Application.Contracts.Identity;

public interface ICreateRefreshTokenService
{
    string CreateRefreshToken();
}