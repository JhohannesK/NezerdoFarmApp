namespace NezerdoFarmApp.Models
{
	public class EggProduction
	{
		public int EggProductionId { get; set; }
		public int LivestockId { get; set; }
		public Livestock Livestock { get; set; }
		public required DateTime DateofProduction { get; set; } = DateTime.UtcNow;
		public required int Quantity { get; set; }
	}
}
