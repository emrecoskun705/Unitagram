namespace Identity.Application.Users.Commands.AuthenticateUser;

public record AuthenticationDto(string AccessToken, string RefreshToken);