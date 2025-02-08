using Microsoft.EntityFrameworkCore;
using NezerdoFarmApp.Interfaces;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Services;

public class FarmRoleService: IFarmRoleService
{
    private readonly ApplicationDbContext _context;

    public async Task<Result<FarmRole>> CreateRoleAsync(Guid farmId, string roleName,
        Dictionary<string, List<ActionType>> resourcePermissions, string? description)
    {
        // This should create a new role
        var role = new FarmRole
        {
            FarmId = farmId,
            RoleName = roleName,
            Description = description
        };

        _context.FarmRoles.Add(role);
        await _context.SaveChangesAsync();
        
        // Create permissions for each resource listed in the `resourcePermissions` dictionary
        foreach (var resourcePerm in resourcePermissions)
        {
            var resource = await _context.Resources.FirstOrDefaultAsync(r => r.ResourceName == resourcePerm.Key);

            if (resource != null)
            {
                foreach (var action in resourcePerm.Value)
                {
                    var permission = await _context.Permissions
                        .FirstOrDefaultAsync(p => p.ResourceId == resource.ResourceId &&
                                                  p.Action == action);

                    if (permission != null)
                    {
                        _context.FarmRolePermissions.Add(new FarmRolePermission
                        {
                            FarmRoleId = role.Id,
                            PermissionId = permission.PermissionId
                        });
                    }
                }
            }
        }
        {
            
        }

        await _context.SaveChangesAsync();
        return Result.Success(role);
    }
}