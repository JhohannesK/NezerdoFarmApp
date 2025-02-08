namespace NezerdoFarmApp.Models
{
	public class FeedConsumption
	{
		public int FeedConsumptionId { get; set; }
		public int FeedId { get; set; }
		public Feed Feed { get; set; }
		public decimal Quantity { get; set; }
		public DateTime ConsumptionDate { get; set; }
	}
}
