using System.ComponentModel.DataAnnotations.Schema;

namespace NezerdoFarmApp.Models
{
	public class Resource
	{
		public int ResourceId { get; set; }
		public required string ResourceName { get; set; }
		public string? Description { get; set; }
	}

	public class Permission
	{
		public int PermissionId { get; set; }
		public int ResourceId { get; set; }
		public ActionType Action { get; set; }

		public virtual Resource Resources { get; set; }
		public virtual ICollection<FarmRolePermission> FarmRolePermissions {get; set;}
	}

	public enum ActionType
	{
		Read = 1,
		Write = 2
	}
}
