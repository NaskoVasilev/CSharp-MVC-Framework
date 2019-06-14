using System.ComponentModel.DataAnnotations;

namespace Musaca.Data.Models
{
	public class OrderProduct
	{
		[Required]
		public string ProductId { get; set; }
		public Product Product { get; set; }

		[Required]
		public string OrderId { get; set; }
		public Order Order { get; set; }
	}
}
