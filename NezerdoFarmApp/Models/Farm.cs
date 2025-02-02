namespace NezerdoFarmApp.Models
{
	public class Farm
	{
		public Guid FarmId { get; set; }
		public required string FarmName { get; set; }
		public string FarmLocation { get; set; }
		public string City { get; set; }
		public string? FarmSize { get; set; }
		public Guid FarmOwner { get; set; }

		public virtual ICollection<User> Users { get; set; }
		public ICollection<Sale> Sales {get;set;}	
		public ICollection<Feed> Feeds {get;set;}
		public virtual ICollection<FarmUserRole> FarmUserRoles { get; set; }

	}
}
