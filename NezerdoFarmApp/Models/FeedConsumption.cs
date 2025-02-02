namespace NezerdoFarmApp.Models
{
	public class FeedConsumption
	{
		public Guid FeedConsumptionId { get; set; }
		public Guid FeedId { get; set; }
		public Feed Feed { get; set; }
		public decimal Quantity { get; set; }
		public DateTime ConsumptionDate { get; set; }
	}
}
