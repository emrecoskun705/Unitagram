namespace Identity.WebAPI.Endpoints.Users.Requests;

public record AuthenticateUserRequest(string Username, string Password);