using NezerdoFarmApp.Models;

namespace NezerdoFarmApp.DTOs;

public class FarmRoleDTO
{
    public int Id { get; set; }
    public string Rolename { get; set; }
    public string? Description { get; set; }
}
public class CreateRoleRequest
{
    public required string FarmId { get; set; }
    public required string RoleName { get; set; }
    public string? Description { get; set; }
    public required Dictionary<string, List<ActionType>> Permissions { get; set; }
}
