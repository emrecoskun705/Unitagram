using Identity.Application.Abstractions.Messaging;

namespace Identity.Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand(string Username, string Password) : ICommand<AuthenticationDto>;