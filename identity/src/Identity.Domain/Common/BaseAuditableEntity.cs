using Unitagram.Domain.Common;

namespace Identity.Domain.Common;

public abstract class BaseAuditableEntity<TEntityId> : BaseEntity<TEntityId>
{
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}