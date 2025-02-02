namespace NezerdoFarmApp.Models
{
	public class Livestock
	{
		public Guid LivestockId { get; set; }
		public required Guid FarmId { get; set; }
		public Farm Farm { get; set; }
		public required string LiveStockType { get; set; }
		public string? Breed { get; set; }
		public required string BatchNumber { get; set; }
		public required DateTime StartDate { get; set; }
		public DateTime? EndTime { get; set; }
		public required int NumberOfAnimals { get; set; }
		public EggProduction? EggProduction { get; set; }
		public ICollection<HealthRecord> HealthRecords { get; set; }
	}
}
