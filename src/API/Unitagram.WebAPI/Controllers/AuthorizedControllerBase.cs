using Microsoft.AspNetCore.Mvc;

namespace Unitagram.WebAPI.Controllers;

/// <summary>
/// Email Authorized Confirmation filter
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
// [ServiceFilter(typeof(EmailConfirmationFilter))]
public class AuthorizedControllerBase : ControllerBase
{
}