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

public class AuthService(ApplicationDbContext dataContext, IConfiguration _configuration, UserManager<User> userManager, IHttpContextAccessor _httpContextAccessor) : IAuthService
{
    public async Task<Result<SignInAuthResponse>> SignInAction(SignInDto signInDTO)
    {
        var user = await userManager.FindByEmailAsync(signInDTO.Email);

        if (user == null)
        {
            return Result.Failure<SignInAuthResponse>("Invalid Credentials");
        }
        var mappedUser = AuthMappings.MapToUserDto(user);

        if (!BCrypt.Net.BCrypt.Verify(signInDTO.Password, user.PasswordHash))
        {
            return Result.Failure<SignInAuthResponse>("Invalid Credentials");
        }

        var token = GenerateJwtToken(user);
        var context = _httpContextAccessor.HttpContext.Response;
        context.Cookies.Append("token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        });
        
        var response = new SignInAuthResponse
        {
            User = AuthMappings.MapToUserDto(user),
        };
        return Result.Success<SignInAuthResponse>(response);
    }

    public async Task<Result<string>> SignUpAction(SignUpDto signUpDTO)
    {
        if (!Enum.TryParse<Roles>(signUpDTO.Role, true, out var userRole))
        {
            return Result.Failure<string>("Invalid Role provided. Accepted values are 'User' or 'Admin'");
        }
        if (await CheckUserExist(signUpDTO.Email))
        {
            return Result.Success("User already exits with this email.");
        }
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(signUpDTO.Password);
        var user = new User
        {
            Email = signUpDTO.Email,
            FirstName = signUpDTO.FirstName,
            LastName = signUpDTO.LastName, 
            PasswordHash= hashedPassword,
            UserName = signUpDTO.FirstName,
            MiddleName = signUpDTO.MiddleName,
            PhoneNumber = signUpDTO.PhoneNumber
        };
        
        var result = await userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, signUpDTO.Role);
            return Result.Success("Sign up successful!");
        }
        // await dataContext.SaveChangesAsync();

        return Result.Success("Could not Sign up user");
    }
    
    #region Helper methods
    private async Task<bool> CheckUserExist(string email)
    {
        return await dataContext.Users.AnyAsync(u => u.Email == email);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                // REVIEW: Check to see how best I can add the permissions as claims.
                // new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"], 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    #endregion
}