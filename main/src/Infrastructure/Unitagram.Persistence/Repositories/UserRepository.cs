using Unitagram.Domain.Users;
using Unitagram.Persistence.Data;

namespace Unitagram.Persistence.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}