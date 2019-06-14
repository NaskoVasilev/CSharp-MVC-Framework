using MvcFramework.Attributes.Validation;

namespace Musaca.Models.Orders
{
	public class OrderInputModel
	{
		[Required]
		[StringLength(5, 20)]
		public string Product { get; set; }

		public string OrderId { get; set; }
	}
}
