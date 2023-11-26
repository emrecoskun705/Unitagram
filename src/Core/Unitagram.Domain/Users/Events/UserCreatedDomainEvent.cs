using Unitagram.Domain.Common;

namespace Unitagram.Domain.Users.Events;

public record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
