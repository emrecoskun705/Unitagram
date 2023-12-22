namespace Identity.WebAPI.Endpoints.Users.Requests;

public record CreateUserRequest(string Email, string Username, string Password);