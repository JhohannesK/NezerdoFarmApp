namespace NezerdoFarmApp.Models
{
	public sealed class FarmUserRole
	{
		public string UserId { get; set; }
		public Guid FarmId { get; set; }
		public int FarmRoleId { get; set; }

		public FarmRole FarmRole { get; set; }
		public Farm Farm { get; set; }
		public User User { get; set; }
	}
}
