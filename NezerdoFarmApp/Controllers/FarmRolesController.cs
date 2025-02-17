﻿using Microsoft.AspNetCore.Mvc;
using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Interfaces;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Services;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Controllers;

[Route("api/v1/farm-roles")]
[ApiController]

public class FarmRolesController(IFarmRoleService farmRoleService): ControllerBase
{

    [HttpPost("create-role")]
    public async Task<ActionResult<FarmRole>> CreateRole([FromBody] CreateRoleRequest request)
    {
        Guid farmId;
        var checkId = Guid.TryParse(request.FarmId, out farmId);
        if (!checkId)
        {
            return BadRequest(Task.FromResult(Result.Failure("Id is not found")).Result);
        }

        var result = await farmRoleService.CreateRoleAsync(farmId, request.RoleName, request.Permissions, request.Description);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}