using System.Security.Cryptography;
using System.Text;

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
    public string HashedPassword { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
    public IEnumerable<UserRole> UserRoles { get; }

    private User()
    {
    }

    public static User Create(string email, string username, string password, DateTimeOffset createTime)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailVerified = false,
            Active = true,
            Username = username,
            NormalizedUsername = username.ToUpper(),
            HashedPassword = password, // make it hashed
            CreatedDateTime = createTime,
            UpdatedDateTime = null,
        };
    }
    
}