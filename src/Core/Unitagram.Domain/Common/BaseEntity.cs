using System.ComponentModel.DataAnnotations.Schema;

namespace Unitagram.Domain.Common;

public abstract class BaseEntity<TEntityId>
{
    public TEntityId Id { get; init; } = default!;

    private readonly List<BaseEvent> _domainEvents = new();
    
    protected BaseEntity(TEntityId id) => Id = id;

    protected BaseEntity() {

    }

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}