using Identity.Domain.Roles.ValueObjects;
using Identity.Domain.Users;

namespace Identity.Domain.Roles;

public sealed class Role
{
    public RoleId Id { get; set; }
    public Name Name { get; set; }
    public NormalizedName NormalizedName { get; set; }
    public IEnumerable<UserRole>? UserRoles { get; }
}