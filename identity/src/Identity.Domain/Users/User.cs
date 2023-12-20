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
    public Password Password { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
    public IEnumerable<UserRole> UserRoles { get; }
    
    public IdentityId IdentityId { get; private set; }

    private User(UserId id, Email email, Username username, Password password, DateTimeOffset createdDateTime, IdentityId identityId) : base(id)
    {
        Email = email;
        NormalizedEmail = NormalizedEmail.Create(email.Value);
        Username = username;
        NormalizedUsername = NormalizedUsername.Create(username.Value);
        Password = Password.Hash(password.Value);
        EmailVerified = false;
        Active = true;
        CreatedDateTime = createdDateTime;
        IdentityId = identityId;
    }
    
    public static User Create(Email email, Username username, Password password, DateTimeOffset createTime)
    {
        var user = new User(UserId.New(), email, username, password, createTime, IdentityId.New());
        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
    
}