using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitagram.Application.Users.RegisterUser;

namespace Unitagram.WebAPI.Controllers.v1.Accounts;


/// <summary>
/// Account authentication controller
/// </summary>
[AllowAnonymous]
[Asp.Versioning.ApiVersion("1.0")]
public class AccountController : CustomControllerBase
{
    private readonly ISender _sender;
    
    public AccountController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.UserName,
            request.FirstName,
            request.LastName,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    
}