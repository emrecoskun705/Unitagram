using System.ComponentModel.DataAnnotations.Schema;

namespace Unitagram.Domain.Common;

public abstract class BaseEntity<TEntityId>
{
    public TEntityId Id { get; init; } = default!;

    private readonly List<IDomainEvent> _domainEvents = new();
    
    protected BaseEntity(TEntityId id) => Id = id;

    protected BaseEntity() {

    }

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

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
}