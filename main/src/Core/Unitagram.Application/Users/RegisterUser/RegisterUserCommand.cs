﻿using Unitagram.Application.Contracts.Messaging;

namespace Unitagram.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword) : ICommand<Guid>;