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

        _ = root.MapPost("/", CreateUser)
            .Produces<Guid>()
            .ProducesProblem(StatusCodes.Status400BadRequest);

        return app;
    }
    
    private static async Task<IResult> CreateUser([FromBody]CreateUserRequest request, IMediator mediator)
    {
        try
        {
            var result = await mediator.Send(new CreateUserCommand(request.Email, request.Username, request.Password));

            if (result.IsSuccess)
                return Results.Ok(result);
            
            return Results.BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
}