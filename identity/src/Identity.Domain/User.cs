namespace Identity.Domain;

public class User
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailVerified { get; set; }
    public bool Active { get; set; }
    public string Username { get; set; }
    public string NormalizedUsername { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
    public IEnumerable<UserRole> UserRoles { get; }
}