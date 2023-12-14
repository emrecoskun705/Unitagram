using Unitagram.Application.Contracts.Authentication;
using Unitagram.Application.Contracts.Messaging;
using Unitagram.Domain.Common;
using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;
using Unitagram.Domain.Users.ValueObjects;

namespace Unitagram.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly ICreateUserService _createUserService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterUserCommandHandler(
        ICreateUserService createUserService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _createUserService = createUserService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            new FirstName(string.Empty), // firstname empty
            new LastName(string.Empty), // lastname empty
            new Email(request.Email),
            new UserName(request.UserName));
        
        var identityId = await _createUserService.CreateUserAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(identityId.Value);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}