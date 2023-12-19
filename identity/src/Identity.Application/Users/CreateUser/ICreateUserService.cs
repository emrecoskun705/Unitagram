using Identity.Domain.Shared;

namespace Identity.Application.Users.CreateUser;

public interface ICreateUserService
{
    Task<Result> CreateUser(CreateUserRequest request, CancellationToken cancellationToken = default);
}