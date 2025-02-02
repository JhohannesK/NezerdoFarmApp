namespace NezerdoFarmApp.Models
{
	public class Expense
	{
		public Guid ExpenseId { get; set; }
		public string ItemName { get; set; }
		public Guid? FeedId { get; set; }
		public Feed Feed { get; set; }
		public Guid? LivestockId { get; set; }
		public Livestock Livestock { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime ExpenseDate { get; set; }
	}
}
