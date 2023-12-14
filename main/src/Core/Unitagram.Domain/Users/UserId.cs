namespace Unitagram.Domain.Users;

public record UserId(Guid Value)
{
    public static UserId FromValue(Guid value) => new UserId(value);

    public static UserId New() => FromValue(Guid.NewGuid());
}