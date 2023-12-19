using Unitagram.Domain.Common;

namespace Identity.Domain.Common;

public abstract class BaseEntity<TEntityId> : IEntity
{
    public TEntityId Id { get; init; } = default!;

    private readonly List<IDomainEvent> _domainEvents = new();
    
    protected BaseEntity(TEntityId id) => Id = id;

    protected BaseEntity() {

    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }
}