namespace Unitagram.Application.Contracts.Authentication;

public interface ILogoutUserService
{
    Task<bool> LogoutUser(string refreshToken, CancellationToken cancellationToken = default);
}