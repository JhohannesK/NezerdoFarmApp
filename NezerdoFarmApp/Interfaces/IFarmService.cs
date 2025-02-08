using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Interfaces;

public interface IFarmService
{
    Task<Result<Farm>> CreateFarmAsync(CreateFarmDto createFarmDto);
}