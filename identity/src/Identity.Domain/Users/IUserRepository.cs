using Identity.Domain.Roles;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    void Add(User user);
    Task AddRoleAsync(UserRole role);
    Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
}