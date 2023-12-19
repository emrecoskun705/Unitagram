using Identity.Domain.Common;
using Identity.Domain.Users.Events;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Domain.Users;

public sealed class User : BaseEntity<UserId>
{
    public UserId Id { get; set; }
    public Email? Email { get; set; }
    public NormalizedEmail? NormalizedEmail { get; set; }
    public bool EmailVerified { get; set; }
    public bool Active { get; set; }
    public Username Username { get; set; }
    public NormalizedUsername NormalizedUsername { get; set; }
    public HashedPassword HashedPassword { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
    public IEnumerable<UserRole> UserRoles { get; }
    
    public string IdentityId { get; private set; } = string.Empty;

    private User(UserId id, Email email, Username username, Password password, DateTimeOffset createTime) : base(id)
    {
        Email = email;
        NormalizedEmail = NormalizedEmail.Create(email.Value);
        Username = username;
        NormalizedUsername = NormalizedUsername.Create(username.Value);
        HashedPassword = HashedPassword.Create(password.Value);
        EmailVerified = false;
        Active = true;
        CreatedDateTime = createTime;
    }
    
    public void SetIdentityId(string identityId) {
        IdentityId = identityId;
    }

    public static User Create(Email email, Username username, Password password, DateTimeOffset createTime)
    {
        var user = new User(UserId.New(), email, username, password, createTime);
        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
    
}