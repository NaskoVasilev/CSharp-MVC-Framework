using Musaca.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace Musaca.Models.Orders
{
	public class OrderViewModel
	{
		public string Id { get; set; }

		public ICollection<ProductViewModel> Products { get; set; }

		public decimal Total => this.Products.Sum(p => p.Price);
	}
}
