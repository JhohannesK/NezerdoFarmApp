namespace NezerdoFarmApp.Models
{
	public class Feed
	{
		public int FeedId { get; set; }
		public required string FeedName { get; set; }
		public decimal Weight { get; set; }
		public string? Type { get; set; }
		public Guid FarmId { get; set; }
		public Farm Farm { get; set; }
		public ICollection<FeedConsumption> FeedConsumptions { get; set; }
		public Expense Expenses { get; set; }
	}
}
