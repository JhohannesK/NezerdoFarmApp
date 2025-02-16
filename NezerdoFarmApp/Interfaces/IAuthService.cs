using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Interfaces;

public interface IAuthService
{
    Task<Result<UserDto>> SignInAction(SignInDto signInDto);
    Task<Result<string>> AdminSignUpActionAsync(SignUpDto signUpDto); 
    Task<Result<string>> AddUserToFarmAsync(AddUserSignUpDto signUpDto);
}