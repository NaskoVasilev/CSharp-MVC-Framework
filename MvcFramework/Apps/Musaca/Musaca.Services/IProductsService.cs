using Musaca.Data.Models;
using Musaca.Models.Products;
using System.Collections.Generic;

namespace Musaca.Services
{
	public interface IProductsService
	{
		Product Create(Product product);

		List<ProductViewModel> GetAll();

		string GetProductByName(string name);
	}
}
