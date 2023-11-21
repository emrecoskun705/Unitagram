using Microsoft.AspNetCore.Mvc;

namespace Unitagram.WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
// [ServiceFilter(typeof(EmailConfirmationFilter))]
public class AuthorizedControllerBase : ControllerBase
{
}