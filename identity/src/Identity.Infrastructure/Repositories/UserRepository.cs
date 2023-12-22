using Identity.Domain.Users;
using Identity.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default)
    {
        return await _db.Set<User>()
            .FirstOrDefaultAsync(x => x.NormalizedUsername == NormalizedUsername.Create(username.Value), cancellationToken: ct);
    }
    
    public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
    {
        return await _db.Set<User>()
            .FirstOrDefaultAsync(x => x.NormalizedEmail == NormalizedEmail.Create(email.Value), cancellationToken: ct);
    }
}