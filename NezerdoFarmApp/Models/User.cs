using Microsoft.AspNetCore.Identity;

namespace NezerdoFarmApp.Models
{
	public class User:IdentityUser
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string? MiddleName { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public Guid? FarmId { get; set; }

		public virtual Farm Farm { get; set; }
		public virtual ICollection<FarmUserRole> FarmUserRoles { get; set; }

	}
}
