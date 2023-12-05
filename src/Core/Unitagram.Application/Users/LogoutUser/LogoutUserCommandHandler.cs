using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Contracts.Messaging;
using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;

namespace Unitagram.Application.Users.LogoutUser;

public class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand, bool>
{
    private readonly ILogoutUserService _logoutUserService;

    public LogoutUserCommandHandler(ILogoutUserService logoutUserService)
    {
        _logoutUserService = logoutUserService;
    }

    public async Task<Result<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _logoutUserService.LogoutUser(
            request.RefreshToken,
            cancellationToken);
        
        if (!result) 
        {
            return Result.Failure<bool>(UserErrors.InvalidCredentials);
        }

        return true;
    }
}