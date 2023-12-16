namespace Identity.Domain;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<UserRole>? UserRoles { get; }
}