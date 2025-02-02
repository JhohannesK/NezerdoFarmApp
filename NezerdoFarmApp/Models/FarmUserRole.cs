namespace NezerdoFarmApp.Models
{
	public class FarmUserRole
	{
		public string UserId { get; set; }
		public Guid FarmId { get; set; }
		public int FarmRoleId { get; set; }

		public virtual FarmRole FarmRole { get; set; }
		public virtual Farm Farm { get; set; }
		public virtual User User { get; set; }
	}
}
