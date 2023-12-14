using Unitagram.Application.Contracts.Messaging;

namespace Unitagram.Application.Users.LogoutUser;

public sealed record LogoutUserCommand(string RefreshToken) : ICommand<bool>;