using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitagram.Application.Contracts.Common;
using Unitagram.Application.Models;
using Unitagram.Application.Users.LoginUser;
using Unitagram.Application.Users.LogoutUser;
using Unitagram.Application.Users.RefreshToken;
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
    private readonly ILocalizationService _localizationService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="localizationService"></param>
    public AccountController(ISender sender, ILocalizationService localizationService)
    {
        _sender = sender;
        _localizationService = localizationService;
    }
    
    /// <summary>
    /// Handles user registration requests.
    /// </summary>
    /// <param name="request">The registration request data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns an IActionResult representing the result of the registration operation.</returns>
    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.UserName,
            request.Password,
            request.ConfirmPassword);

        var result = await _sender.Send(command, cancellationToken);

        return result.ToOk(_localizationService);
    }
    
    /// <summary>
    /// Handles user login requests.
    /// </summary>
    /// <param name="request">Login request data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<AccessTokenResponse>> Login(
        LoginUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var result = await _sender.Send(command, cancellationToken);
        
        return result.ToOk(_localizationService);
    }
    
    /// <summary>
    /// Refreshes the access token using the provided refresh token.
    /// </summary>
    /// <param name="request">The refresh token request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the result of the access token refresh operation.</returns>
    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshTokenResponse>> Refresh(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request.Token);

        var result = await _sender.Send(command, cancellationToken);
        
        return result.ToOk(_localizationService);
    }

    /// <summary>
    /// Logs out the user based on the provided refresh token.
    /// </summary>
    /// <param name="request">The logout user request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns IActionResult representing the result of the logout operation.</returns>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        LogoutUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LogoutUserCommand(request.RefreshToken);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest();
        
        return NoContent();
    }

    
}