using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Shared;
using Identity.Domain.Users;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(TimeProvider dateTimeProvider) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            new Email(request.Email),
            new Username(request.Username),
            Password.Hash(request.Password),
            dateTimeProvider.GetUtcNow());


        return Result.Success(Guid.NewGuid());
    }
}