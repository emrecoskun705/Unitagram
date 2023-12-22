using Identity.Domain.Roles;
using Identity.Domain.Roles.ValueObjects;
using Identity.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

internal sealed class RoleRepository : Repository<Role, RoleId>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _db.Set<Role>()
            .FirstOrDefaultAsync(x => x.NormalizedName == NormalizedName.Create(name), cancellationToken: cancellationToken);
    }
}