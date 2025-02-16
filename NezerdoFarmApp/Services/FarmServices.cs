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
             await context.SaveChangesAsync();
             
             // Create a default farm role for every new farm created
             var farmRole = new FarmRole
             {
                 FarmId = farm.FarmId,
                 Description = $"This is the owner of {farm.FarmName}",
                 RoleName = "FarmOwner"
             };
             context.FarmRoles.Add(farmRole);
             await context.SaveChangesAsync();
             var farmRoleId = farmRole.Id;

             var getPermissionIdWithAdminAction =
                await  context.Permissions.FirstOrDefaultAsync(p => p.Action == ActionType.Admin);

             if (getPermissionIdWithAdminAction == null)
                 return Result.Failure<FarmDto>("No permission with admin action exist");
             context.FarmRolePermissions.Add(new FarmRolePermission
             {
                 FarmRoleId = farmRoleId,
                 PermissionId = getPermissionIdWithAdminAction.PermissionId
             });
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

    public async Task<bool> CreateOrAddResource(List<string> resources)
    {
        foreach (var resource in resources)
        {
            var ifExist = await CheckIfResourceExist(resource);
            if (!ifExist)
            {
                context.Resources.Add(new Resource
                {
                    ResourceName = resource.ToUpper()
                });
                await context.SaveChangesAsync();
            }
        }

        return true;
    }

    public async Task<Result<ICollection<FarmDto>>> GetAllFarmsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<FarmDto>> UpdateFarmDetailsAsync(string farmId)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> CheckIfFarmExist(string farmName)
    {
        var farm = await context.Farms.FirstOrDefaultAsync(f => f.FarmName == farmName);
        return farm != null;
    }

    private async Task<bool> CheckIfResourceExist(string resourceName)
    {
        var resource = await context.Resources.FirstOrDefaultAsync(r => r.ResourceName == resourceName);
        return resource != null;
    }
}