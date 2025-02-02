namespace NezerdoFarmApp.Models
{
	public class Feed
	{
		public Guid FeedId { get; set; }
		public required string FeedName { get; set; }
		public decimal weight { get; set; }
		public string? type { get; set; }
		public Guid FarmId { get; set; }
		public Farm Farm { get; set; }
		public ICollection<FeedConsumption> FeedConsumptions { get; set; }
		public Expense Expenses { get; set; }
	}
}
