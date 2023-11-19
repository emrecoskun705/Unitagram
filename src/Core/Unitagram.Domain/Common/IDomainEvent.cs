using MediatR;

namespace Unitagram.Domain.Common;

public interface IDomainEvent : INotification
{
    Guid Id { get; init; }

    DateTime OccurredOnUtc { get; init; }
}