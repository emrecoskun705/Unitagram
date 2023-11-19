using MediatR;

namespace Unitagram.Domain.Common;

public abstract class BaseEvent : INotification
{
    protected BaseEvent(Guid id, DateTime occurredOnUtc)
        : this()
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
    }

    private BaseEvent()
    {
    }
    
    public Guid Id { get; init; }
    public DateTime OccurredOnUtc { get; init; }
}