using Unitagram.Application.Contracts.Messaging;

namespace Unitagram.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;