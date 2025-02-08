using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Interfaces;

public interface IAuthService
{
    Task<Result<SignInAuthResponse>> SignInAction(SignInDto signInDTO);
    Task<Result<string>> SignUpAction(SignUpDto signUpDTO);
}