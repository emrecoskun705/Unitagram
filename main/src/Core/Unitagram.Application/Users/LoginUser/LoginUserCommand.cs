using Unitagram.Application.Contracts.Messaging;
using Unitagram.Application.Models;

namespace Unitagram.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;