namespace NezerdoFarmApp.Models
{
	public class FarmRole
	{
		public int Id { get; set; }
		public Guid FarmId { get; set; }
		public string RoleName { get; set; }
		public string? Description { get; set; }
		
		public virtual Farm Farm { get; set; }
		public virtual ICollection<FarmRolePermission> RolePermissions { get; set; }
		public virtual ICollection<FarmUserRole> FarmUserRoles { get; set; }
	}
}
