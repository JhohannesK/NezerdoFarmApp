using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Models;

namespace NezerdoFarmApp.Mappings;

public class FarmMappings
{
    public static Farm MapToFarm(CreateFarmDto farm, string userId)
    {
        return new Farm()
        {
            FarmName = farm.FarmName,
            FarmOwner = userId,
            City = farm.City,
            FarmLocation = farm.FarmLocation,
            FarmSize = farm.FarmSize
        };
    }

    public static FarmDto MapToFarmDto(Farm farm)
    {
        return new FarmDto()
        {
            FarmId = farm.FarmId,
            FarmName = farm.FarmName,
            City = farm.City,
            FarmLocation = farm.FarmLocation,
            FarmSize = farm.FarmSize
        };
    }
}