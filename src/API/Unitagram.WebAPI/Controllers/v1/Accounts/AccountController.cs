using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitagram.Application.Users.RegisterUser;

namespace Unitagram.WebAPI.Controllers.v1.Accounts;


/// <summary>
/// Controller for handling account authentication operations.
/// </summary>
[AllowAnonymous]
[ApiVersion("1.0")]
public class AccountController : CustomControllerBase
{
    private readonly ISender _sender;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="sender">The MediatR sender for handling commands.</param>
    public AccountController(ISender sender)
    {
        _sender = sender;
    }
    
    /// <summary>
    /// Handles user registration requests.
    /// </summary>
    /// <param name="request">The registration request data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns an IActionResult representing the result of the registration operation.</returns>
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