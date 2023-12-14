namespace Unitagram.WebAPI.Controllers.v1.Accounts;

public sealed record RegisterUserRequest(
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword);