using Identity.Domain.Common;
using Identity.Domain.Roles.ValueObjects;
using Identity.Domain.Users;

namespace Identity.Domain.Roles;

public sealed class Role : BaseEntity<RoleId>
{
    public const string DefaultUser = nameof(DefaultUser); 
    public const string Administrator = nameof(Administrator); 
    public RoleId Id { get; set; }
    public Name Name { get; set; }
    public NormalizedName NormalizedName { get; set; }
    public IEnumerable<UserRole>? UserRoles { get; }

    private Role(RoleId id, Name name)
    {
        Id = id;
        Name = name;
        NormalizedName = NormalizedName.Create(name.Value);
    }

    public static Role Create(RoleId roleId, Name name)
    {
        return new Role(roleId, name);
    }
}