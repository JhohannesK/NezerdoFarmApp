using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.IdentityModel.Tokens;
using NezerdoFarmApp.Enums;

namespace NezerdoFarmApp.DTOs;

public class UserDto
{
    public required string Id { get; set;}
    public required string? UserName { get; set;}
    public required string FirstName { get; set;}
    public required string LastName { get; set;}
    public required string Email { get; set;}
    public DateTime CreatedAt { get; set;}
    public DateTime UpdateAt { get; set;}
}

public class SignInDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignInAuthResponse
{
    public required UserDto? User { get; set; }
}

public class SignUpDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set;}
    public string? MiddleName {get; set;}
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public required string PhoneNumber { get; set; }
    public string Role { get; set; } = "User";
}


public class  DeleteUserDto: SignUpDto
{
    public required int Id { get; set; }

}

