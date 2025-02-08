
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NezerdoFarmApp.Controllers;
[Route("api/v1/farm")]
[ApiController]
public class FarmController : ControllerBase
{
    [HttpPost("create-farm")]
    [Authorize]
    public async Task<IActionResult> CreateFarm()
    {
        var username = User.Identity?.Name;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        return Ok(new {username, email});
    }
}