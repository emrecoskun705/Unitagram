using Identity.Application.Users.Commands.AuthenticateUser;
using Identity.Application.Users.Commands.CreateUser;
using Identity.WebAPI.Endpoints.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebAPI.Endpoints.Users;

public static class UsersEndpoints
{
    public static WebApplication MapUsersEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/user")
            .WithTags("users");

        _ = root.MapPost("/create", CreateUser)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        
        _ = root.MapPost("/authenticate", AuthenticateUser)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        return app;
    }

    private static async Task<IResult> CreateUser([FromBody] CreateUserRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new CreateUserCommand(request.Email, request.Username, request.Password));

        if (result.IsSuccess)
            return Results.Created();

        return Results.BadRequest(result.Error);
    }

    private static async Task<IResult> AuthenticateUser([FromBody] AuthenticateUserRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new AuthenticateUserCommand(request.Username, request.Password));

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return Results.BadRequest(result.Error);
    }
}