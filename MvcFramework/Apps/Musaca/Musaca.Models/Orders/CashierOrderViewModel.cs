namespace Musaca.Models.Orders
{
	public class CashierOrderViewModel
	{
		public string Id { get; set; }

		public decimal Total { get; set; }

		public string Cashier { get; set; }

		public string IssuedOn { get; set; }
	}
}
