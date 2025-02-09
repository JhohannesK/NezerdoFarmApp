
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Interfaces;

namespace NezerdoFarmApp.Controllers;
[Route("api/v1/farm")]
[ApiController]
public class FarmController(IFarmService farmService) : ControllerBase
{
    [HttpPost("create-farm")]
    [Authorize]
    public async Task<IActionResult> CreateFarm([FromBody] CreateFarmDto createFarmDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var result = await farmService.CreateFarmAsync(createFarmDto, userId);
        
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}