using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Interfaces;

public interface IFarmService
{
    Task<Result<FarmDto>> CreateFarmAsync(CreateFarmDto createFarmDto, string userId);
    Task<Result<ICollection<FarmDto>>> GetAllFarmsAsync();
    Task<Result<FarmDto>> UpdateFarmDetailsAsync(string farmId);
    Task<bool> CreateOrAddResource(List<string> resources);
}