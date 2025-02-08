using Microsoft.AspNetCore.Mvc;
using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Interfaces;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(IAuthService authenticationService): ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
    {
        if (signUpDto.Password != signUpDto.ConfirmPassword)
        {
            return BadRequest("Password and Confirm Password do not match");
        }

        var result = await authenticationService.SignUpAction(signUpDto);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("signin")]
    public async Task<ActionResult<UserDto>> SignIn([FromBody] SignInDto signInDto)
    {
        var result = await authenticationService.SignInAction(signInDto);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult<Result<string>>> Logout()
    {
        Response.Cookies.Delete("token");
        return Ok(Result.Success("Logout user successfully"));
    }
}