namespace NezerdoFarmApp.Models
{
	public class FarmRolePermission
	{
		public int FarmRoleId { get; set; }
		public int PermissionId { get; set; }

		public virtual FarmRole FarmRole { get; set; }
		public virtual Permission Permission { get; set; }
	}
}
