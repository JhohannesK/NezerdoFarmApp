using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NezerdoFarmApp.Models;

namespace NezerdoFarmApp
{
	public class ApplicationDbContext: IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
		{
		}
		
		public DbSet<Farm> Farms { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Feed> Feeds { get; set; }
		public DbSet<FarmRole> FarmRoles { get; set; }
		public DbSet<Livestock> Livestocks { get; set; }
		public DbSet<EggProduction> EggProductions { get; set; }
		public DbSet<FeedConsumption> FeedConsumptions { get; set; }
		public DbSet<HealthRecord> HealthRecords { get; set; }
		public DbSet<Expense> Expenses { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Resource> Resources { get; set; }
		public DbSet<FarmRolePermission> FarmRolePermissions { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<FarmRolePermission>()
				.HasKey(frp => new { frp.FarmRoleId, frp.PermissionId });

			builder.Entity<FarmUserRole>()
				.HasKey(fur => new {fur.UserId, fur.FarmId, fur.FarmRoleId });

			builder.Entity<FarmUserRole>()
				.HasOne(fur => fur.FarmRole)
				.WithMany(fr => fr.FarmUserRoles)
				.HasForeignKey(fur => fur.FarmRoleId);

			builder.Entity<FarmUserRole>()
				.HasOne(fur => fur.Farm)
				.WithMany(f => f.FarmUserRoles)
				.HasForeignKey(fur => fur.FarmId);

			builder.Entity<FarmUserRole>()
				.HasOne(fur => fur.User)
				.WithMany(u => u.FarmUserRoles)
				.HasForeignKey(fur => fur.UserId);
			
			builder.Entity<Farm>()
				.Property(f => f.FarmId)
				.HasDefaultValueSql("uuid_generate_v4()");

			builder.Entity<Sale>()
				.Property(s => s.SaleId)
				.HasDefaultValueSql("uuid_generate_v4()");
		}
	}
}
