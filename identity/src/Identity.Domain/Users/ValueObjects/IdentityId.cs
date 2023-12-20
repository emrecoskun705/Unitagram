namespace Identity.Domain.Users.ValueObjects;

public record IdentityId(Guid Value)
{
    public static IdentityId FromValue(Guid value) => new IdentityId(value);

    public static IdentityId New() => FromValue(Guid.NewGuid());
}