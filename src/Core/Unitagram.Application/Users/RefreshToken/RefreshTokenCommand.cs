using Unitagram.Application.Contracts.Messaging;
using Unitagram.Application.Models;

namespace Unitagram.Application.Users.RefreshToken;

public sealed record RefreshTokenCommand(string Token) : ICommand<AccessTokenResponse>;
