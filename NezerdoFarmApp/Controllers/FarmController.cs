
using Microsoft.AspNetCore.Mvc;

namespace NezerdoFarmApp.Controllers;
[Route("api/v1/farm")]
[ApiController]
public class FarmController : ControllerBase
{
    [HttpPost("create-farm")]
    public async Task<IActionResult> CreateFarm()
    {
        return Ok();
    }
}