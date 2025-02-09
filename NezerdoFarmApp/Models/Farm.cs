using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NezerdoFarmApp.Models
{
	public class Farm
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid FarmId { get; set; }
		public required string FarmName { get; set; }
		public string FarmLocation { get; set; }
		public string City { get; set; }
		public string? FarmSize { get; set; }
		public required string FarmOwner { get; set; }

		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<Sale> Sales {get;set;}	
		public virtual ICollection<Feed> Feeds {get;set;}
		public virtual ICollection<FarmUserRole> FarmUserRoles { get; set; }

	}
}
