using Microsoft.AspNetCore.Mvc;

namespace Unitagram.WebAPI.Controllers;

/// <summary>
/// Unauthorized controller base
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CustomControllerBase : ControllerBase
{
}