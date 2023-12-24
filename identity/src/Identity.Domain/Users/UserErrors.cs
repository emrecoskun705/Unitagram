using Identity.Domain.Shared;

namespace Identity.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found");

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
    
    public static Error UsernameExists = new(
        "User.UsernameExists", 
        "Username exists");
    
    public static Error EmailExists = new(
        "User.EmailExists", 
        "Email exists");
    
    public static Error NotActive = new(
        "User.NotActive", 
        "User is not an active user");
}