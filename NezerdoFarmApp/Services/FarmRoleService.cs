using Microsoft.EntityFrameworkCore;
using NezerdoFarmApp.Interfaces;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Services;

public class FarmRoleService(ApplicationDbContext context): IFarmRoleService
{
    public async Task<Result<FarmRole>> CreateRoleAsync(Guid farmId, string roleName,
        Dictionary<string, List<ActionType>> resourcePermissions, string? description)
    {
        var checkRole = await context.FarmRoles.FirstOrDefaultAsync(fr => fr.FarmId == farmId && fr.RoleName == roleName);
        // This should create a new role
        var role = new FarmRole
        {
            FarmId = farmId,
            RoleName = roleName,
            Description = description
        };

        if (checkRole == null)
        {
            context.FarmRoles.Add(role);
            await context.SaveChangesAsync();
        }
        
        // Create permissions for each resource listed in the `resourcePermissions` dictionary
        foreach (var resourcePerm in resourcePermissions)
        {
            var resource = await context.Resources.FirstOrDefaultAsync(r => r.ResourceName == resourcePerm.Key);

            if (resource != null)
            {
                foreach (var action in resourcePerm.Value)
                {
                    var permission = await context.Permissions
                        .FirstOrDefaultAsync(p => p.ResourceId == resource.ResourceId &&
                                                  p.Action == action);

                    if (permission != null)
                    {
                        context.FarmRolePermissions.Add(new FarmRolePermission
                        {
                            FarmRoleId = role.Id,
                            PermissionId = permission.PermissionId
                        });
                    }
                    else
                    {
                        var createPermission = new Permission
                        {
                            Action = action,
                            ResourceId = resource.ResourceId,
                            FarmRolePermissions = new List<FarmRolePermission>()
                        };

                        var farmRolePermission = new FarmRolePermission
                        {
                            FarmRoleId = role.Id,
                            Permission = createPermission
                        };
                      
                        createPermission.FarmRolePermissions.Add(farmRolePermission);
                        context.Permissions.Add(createPermission);

                        context.FarmRolePermissions.Add(new FarmRolePermission
                        {
                            FarmRoleId = role.Id,
                            PermissionId = createPermission.PermissionId
                        });
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        await context.SaveChangesAsync();
        return Result.Success(role);
    }

    public async Task<Result<string>> CreateAdminRolePermission()
    {
        var adminResource = await context.Resources.FirstOrDefaultAsync(r => r.ResourceName ==  "ALL");
        if (adminResource != null)
        {
            var checkIfAdminActionExist = await context.Permissions.FirstOrDefaultAsync(p => p.Action == ActionType.Admin);
            if (checkIfAdminActionExist != null) return Result.Success("Permission exist");
            var rolePermission = new Permission
            {
                ResourceId = adminResource.ResourceId,
                Action = ActionType.Admin
            };
            context.Permissions.Add(rolePermission);
            await context.SaveChangesAsync();
            return Result.Success("Permission created");

        }

        return Result.Failure<string>("Failed to create permission for admin farm role");
    }

    public async Task<Result<ICollection<FarmRole>>> GetRolesForAFarmAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ICollection<FarmRole>>> DeleteFarmRoleAsync(int farmRoleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ICollection<FarmRole>>> UpdateFarmRolePermissionsAsync(int farmRoleId)
    {
        throw new NotImplementedException();
    }
}