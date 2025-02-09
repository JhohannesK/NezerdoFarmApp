using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Interfaces;
using NezerdoFarmApp.Mappings;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Services;

public class FarmServices(ApplicationDbContext context, ILogger<FarmServices> logger, UserManager<User> userManager):IFarmService
{
    public async Task<Result<FarmDto>> CreateFarmAsync(CreateFarmDto createFarmDto, string userId)
    {
        var farmExists = CheckIfFarmExist(createFarmDto.FarmName).Result;
        if (farmExists) return Result.Failure<FarmDto>($"Farm with name {createFarmDto.FarmName} already exist.");
        try
        {
            var farm = FarmMappings.MapToFarm(createFarmDto, userId);
            
             context.Farms.Add(farm);
             var addFarm = await context.SaveChangesAsync();
             if (addFarm > 0)
             {
                 var user = await userManager.FindByIdAsync(userId);
                 if (user != null)
                 { 
                     user.FarmId = farm.FarmId;
                     var updatedUserResult = await userManager.UpdateAsync(user);

                     if (updatedUserResult.Succeeded) return Result.Success(FarmMappings.MapToFarmDto(farm));
                     logger.LogError("Error occurred while updating the user's FarmId.");
                     return Result.Failure<FarmDto>("An error occurred while updating the user's FarmId.");
                 }
                 else
                 {
                     logger.LogError("User not found");
                     return Result.Failure<FarmDto>("User not found.");
                 }
             }
            return Result.Success(FarmMappings.MapToFarmDto(farm) );

        }
        catch (Exception e)
        {
           logger.LogError(e, "Error Occured while creating a farm:: CreateFarmAsync");
           return Result.Failure<FarmDto>("An error occured while creating the farm");
        }
    }

    private async Task<bool> CheckIfFarmExist(string farmName)
    {
        var farm = await context.Farms.FirstOrDefaultAsync(f => f.FarmName == farmName);
        return farm != null;
    }
}