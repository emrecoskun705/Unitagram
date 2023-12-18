using Identity.Application.Abstractions.Mediator;

namespace Identity.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, string Username, string Password) : ICommand, ICommand<Task>;