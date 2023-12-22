using Identity.Application.Abstractions.Messaging;

namespace Identity.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string Email,
    string Username,
    string Password) : ICommand<Guid>;

