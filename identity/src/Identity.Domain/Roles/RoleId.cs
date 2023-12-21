using Identity.Domain.Common;

namespace Identity.Domain.Roles;

public record RoleId(Guid Value)
{
    public static RoleId FromValue(Guid value) => new RoleId(value);

    public static RoleId New() => FromValue(Guid.NewGuid());
}