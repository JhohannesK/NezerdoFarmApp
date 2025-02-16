using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Interfaces;

public interface IFarmRoleService
{
    Task<Result<FarmRole>> CreateRoleAsync(Guid farmId, string roleName,
        Dictionary<string, List<ActionType>> resourcePermissions, string? description);

    Task<Result<string>> CreateAdminRolePermission();
}