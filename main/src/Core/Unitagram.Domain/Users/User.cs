using Unitagram.Domain.Common;
using Unitagram.Domain.Users.Events;
using Unitagram.Domain.Users.ValueObjects;

namespace Unitagram.Domain.Users;

public class User : BaseEntity<UserId>
{
    private User(UserId id, FirstName firstName, LastName lastName, Email email, UserName userName) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public UserName UserName { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    public static User Create(FirstName firstName, LastName lastName, Email email, UserName userName)
    {
        var user = new User(UserId.New(), firstName, lastName, email, userName);
        user.RemoveDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
    
    public void SetIdentityId(string identityId) {
        IdentityId = identityId;
    }
    
}