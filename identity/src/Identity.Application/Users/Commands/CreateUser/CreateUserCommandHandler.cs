using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Common;
using Identity.Domain.Shared;
using Identity.Domain.Users;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository, 
    TimeProvider dateTimeProvider) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var getUserByUsername = await userRepository.GetByUsernameAsync(new Username(request.Username), cancellationToken);

        if (getUserByUsername != null)
            return UserErrors.UsernameExists;

        var getUserByEmail = await userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);

        if (getUserByEmail != null)
            return UserErrors.EmailExists;
        
        var user = User.Create(
            new Email(request.Email),
            new Username(request.Username),
            Password.Hash(request.Password),
            dateTimeProvider.GetUtcNow());
        
        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Guid.NewGuid();
    }
}