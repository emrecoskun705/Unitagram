using Microsoft.AspNetCore.Mvc;

namespace Unitagram.WebAPI.Controllers;

/// <summary>
/// Custom controller base fot api controller
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CustomControllerBase : ControllerBase
{
}