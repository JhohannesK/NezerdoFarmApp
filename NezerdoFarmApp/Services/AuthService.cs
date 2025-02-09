using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Enums;
using NezerdoFarmApp.Interfaces;
using NezerdoFarmApp.Mappings;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Services;

public class AuthService(ApplicationDbContext dataContext, IConfiguration configuration, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : IAuthService
{
    public async Task<Result<UserDto>> SignInAction(SignInDto signInDto)
    {
        var user = await userManager.FindByEmailAsync(signInDto.Email);

        if (user is not { EmailConfirmed: true })
        {
            return Result.Failure<UserDto>("Invalid Credentials");
        }
        var mappedUser = AuthMappings.MapToUserDto(user);

        if (!BCrypt.Net.BCrypt.Verify(signInDto.Password, user.PasswordHash))
        {
            return Result.Failure<UserDto>("Invalid Credentials");
        }

        var token = GenerateJwtToken(user);
        var context = httpContextAccessor.HttpContext?.Response;
        context?.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        });


        if (mappedUser != null) return Result.Success(mappedUser);
        return Result.Failure<UserDto>("Could not sign in user");
    }

    public async Task<Result<string>> AdminSignUpActionAsync(SignUpDto signUpDto)
    {
        // if (!Enum.TryParse<Roles>(signUpDto.Role, true, out var userRole))
        // {
        //     return Result.Failure<string>("Invalid Role provided. Accepted values are 'User' or 'Admin'");
        // }
        if (await CheckUserExist(signUpDto.Email))
        {
            return Result.Success("User already exits with this email.");
        }
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password);
        var user = new User
        {
            Email = signUpDto.Email,
            FirstName = signUpDto.FirstName,
            LastName = signUpDto.LastName, 
            PasswordHash= hashedPassword,
            UserName = signUpDto.FirstName,
            MiddleName = signUpDto.MiddleName,
            PhoneNumber = signUpDto.PhoneNumber,
            EmailConfirmed = true
        };
        
        var result = await userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
            return Result.Success("Sign up successful!");
        }
        // await dataContext.SaveChangesAsync();

        return Result.Success("Could not Sign up user");
    }

    public async Task<Result<string>> AddUserToFarmAsync(SignUpDto signUpDTO)
    {
        throw new NotImplementedException();
    }
    
    #region Helper methods
    private async Task<bool> CheckUserExist(string email)
    {
        return await dataContext.Users.AnyAsync(u => u.Email == email);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.LastName + " " + user.FirstName)
                // REVIEW: Check to see how best I can add the permissions as claims.
                // new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"], 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    #endregion
}