using Identity.Application.Abstractions.Clock;
using Identity.Domain;
using Identity.Domain.Shared;
using Identity.Domain.Users;

namespace Identity.Application.Users.CreateUser;

internal class CreateUserService(IDateTimeProvider dateTimeProvider) : ICreateUserService
{
    public async Task<Result> CreateUser(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        // var user = User.Create(request.Email, request.Password, request.Password, dateTimeProvider.UtcNow);

        return Result.Success();
    }
}