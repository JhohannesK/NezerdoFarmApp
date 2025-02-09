using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Interfaces;

public interface IAuthService
{
    Task<Result<UserDto>> SignInAction(SignInDto signInDTO);
    Task<Result<string>> AdminSignUpActionAsync(SignUpDto signUpDTO); 
    Task<Result<string>> AddUserToFarmAsync(SignUpDto signUpDTO);
}