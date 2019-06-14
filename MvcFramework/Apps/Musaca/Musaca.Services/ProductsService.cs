using System.Collections.Generic;
using System.Linq;
using Musaca.Data;
using Musaca.Data.Models;
using Musaca.Models.Products;

namespace Musaca.Services
{
	public class ProductsService : IProductsService
	{
		private readonly MusacaDbContext context;

		public ProductsService(MusacaDbContext context)
		{
			this.context = context;
		}

		public Product Create(Product product)
		{
			context.Products.Add(product);
			context.SaveChanges();
			return product;
		}

		public List<ProductViewModel> GetAll()
		{
			return this.context.Products
				.Select(p => new ProductViewModel
				{
					Name = p.Name,
					Price = p.Price
				})
				.ToList();
		}

		public string GetProductByName(string name)
		{
			string productId = context.Products
				.Where(p => p.Name == name)
				.Select(p => p.Id)
				.FirstOrDefault();

			return productId;
		}
	}
}
