using Identity.Domain.Common;

namespace Identity.Domain.Users.Events;

public record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
