using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Common;
using Identity.Domain.Roles;
using Identity.Domain.Shared;
using Identity.Domain.Users;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IRoleRepository roleRepository,
    IUserRepository userRepository, 
    TimeProvider dateTimeProvider) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var error = await CheckUsernameOrEmailExists(command, cancellationToken);
        if (error != null)
            return error;
        
        var getDefaultRole = await roleRepository.GetByNameAsync(Role.DefaultUser, cancellationToken);

        if (getDefaultRole == null)
            throw new NullReferenceException("Default role cannot be null");
        
        var user = User.Create(
            new Email(command.Email),
            new Username(command.Username),
            Password.Hash(command.Password),
            dateTimeProvider.GetUtcNow());

        var userRole = new UserRole()
        {
            User = user,
            Role = getDefaultRole
        };
        
        userRepository.Add(user);

        await userRepository.AddRoleAsync(userRole);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.IdentityId.Value;
    }

    private async Task<Error?> CheckUsernameOrEmailExists(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var getUserByUsername = await userRepository.GetByUsernameAsync(new Username(command.Username), cancellationToken);

        if (getUserByUsername != null)
            return UserErrors.UsernameExists;

        var getUserByEmail = await userRepository.GetByEmailAsync(new Email(command.Email), cancellationToken);

        if (getUserByEmail != null)
            return UserErrors.EmailExists;

        return null;
    }
}