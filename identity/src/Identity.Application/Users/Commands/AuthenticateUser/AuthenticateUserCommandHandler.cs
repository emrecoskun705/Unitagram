using System.Data;
using Dapper;
using Identity.Application.Abstractions.Data;
using Identity.Application.Abstractions.Jwt;
using Identity.Application.Abstractions.Jwt.Models;
using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Shared;
using Identity.Domain.Users;
using Identity.Domain.Users.ValueObjects;

namespace Identity.Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler(
    TimeProvider timeProvider,
    IAccessTokenService accessTokenService,
    IRefreshTokenService refreshTokenService,
    ISqlConnectionFactory sqlConnectionFactory) : ICommandHandler<AuthenticateUserCommand, AuthenticationDto>
{
    public async Task<Result<AuthenticationDto>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var normalizedUsername = NormalizedUsername.Create(request.Username).Value; 
        
        using var connection = sqlConnectionFactory.CreateConnection();

        var user = await GetUser(connection, normalizedUsername);
        
        if (user == null)
        {
            return UserErrors.InvalidCredentials;
        }

        if (!user.Active)
        {
            return UserErrors.NotActive;
        }

        if (!PasswordManager.VerifyHashedPassword(user.Password, request.Password))
        {
            return UserErrors.InvalidCredentials;
        }
        
        user.Roles = await GetUserRoles(connection, user);

        var session = Guid.NewGuid().ToString();
        
        var accessToken = accessTokenService.GenerateAccessToken(
            new AccessTokenRequest(
                user.Username,
                user.Email,
                user.EmailVerified,
                user.Active,
                session,
                user.Roles)
        );

        var refreshToken = refreshTokenService.GenerateRefreshToken(
            new RefreshTokenRequest(
                user.IdentityId.ToString(),
                session)
            );
        
        return new AuthenticationDto(accessToken.Token, refreshToken.Token);
    }

    private async Task<List<string>> GetUserRoles(IDbConnection connection, UserDTO user)
    {
        var userRoleSql = $"""
                           select "Name"
                           FROM "UserRole" ur
                           INNER JOIN "Role" r ON ur."RoleId" = r."Id"
                           WHERE ur."UserId" = @UserId
                           """;
        
        var userRoles = await connection.QuerySingleOrDefaultAsync<List<string>>(
            userRoleSql,
            new
            {
                UserId = user.Id  
            });

        return userRoles ?? new List<string>();
    }

    private async Task<UserDTO?> GetUser(IDbConnection connection, string normalizedUsername)
    {
        const string sql = $"""
                            SELECT
                                "Id" as "{nameof(UserDTO.Id)}",
                                "IdentityId" as "{nameof(UserDTO.IdentityId)}",
                                "Email" as "{nameof(UserDTO.Email)}",
                                "Active" as "{nameof(UserDTO.Active)}",
                                "EmailVerified" as "{nameof(UserDTO.EmailVerified)}",
                                "Username" as "{nameof(UserDTO.Username)}",
                                "Password" as "{nameof(UserDTO.Password)}"
                            FROM "User" u
                            WHERE u."NormalizedUsername" = @NormalizedUsername
                            """;

        var user = await connection.QuerySingleOrDefaultAsync<UserDTO>(
            sql,
            new
            {
                NormalizedUsername = normalizedUsername  
            });

        return user;
    }
}