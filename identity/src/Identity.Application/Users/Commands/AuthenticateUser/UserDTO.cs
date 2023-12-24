namespace Identity.Application.Users.Commands.AuthenticateUser;

public class UserDTO
{
    public Guid Id { get; set; }
    public Guid IdentityId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; } 
    public bool Active { get; set; }
    public bool EmailVerified { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
};