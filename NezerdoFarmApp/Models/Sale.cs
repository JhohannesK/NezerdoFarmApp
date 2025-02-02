namespace NezerdoFarmApp.Models
{
	public class Sale
	{
		public Guid SaleId { get; set; }
		public Guid FarmId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitCost { get; set; }
		public decimal TotalCost { get; set;}
		public DateTime SalesDate { get; set; }

		public virtual Farm Farm { get; set; }
	}

	public enum SaleType
	{
		Egg,
		Livestock,
		Feed,
		Medicine,
		Other
	}
}
