﻿using Identity.Application.Abstractions.Clock;
using Identity.Application.Abstractions.Mediator;
using Identity.Domain;
using Identity.Domain.Shared;

namespace Identity.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.Email, command.Password, command.Password, dateTimeProvider.UtcNow);

        return Result.Success();
    }
}