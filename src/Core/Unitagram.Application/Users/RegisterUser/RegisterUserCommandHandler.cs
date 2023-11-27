using Unitagram.Application.Contracts.Identity;
using Unitagram.Application.Contracts.Messaging;
using Unitagram.Domain.Common;
using Unitagram.Domain.Shared;
using Unitagram.Domain.Users;
using Unitagram.Domain.Users.ValueObjects;

namespace Unitagram.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterUserCommandHandler(
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email),
            new UserName(request.UserName));
        
        var identityId = await _authenticationService.CreateUserAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(identityId.Value);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}