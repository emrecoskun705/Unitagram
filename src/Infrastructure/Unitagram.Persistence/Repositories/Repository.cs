using Microsoft.EntityFrameworkCore;
using Unitagram.Domain.Common;
using Unitagram.Persistence.Data;

namespace Unitagram.Persistence.Repositories;

internal abstract class Repository<TEntity, TEntityId> 
    where TEntity : BaseEntity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken ct = default)
    {
        return await _db.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public void Add(TEntity entity)
    {
        _db.Add(entity);
    }
}