using Unitagram.Domain.Common;

namespace Identity.Domain.Common;

public interface IEntity
{
    void ClearDomainEvents();
    IReadOnlyList<IDomainEvent> GetDomainEvents();
}