using Musaca.Data.Models;
using Musaca.Models.Products;
using Musaca.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.AutoMapper.Extensions;
using MvcFramework.Results;

namespace Musaca.Web.Controllers
{
	[Authorize]
	public class ProductsController : Controller
	{
		private readonly IProductsService productsService;

		public ProductsController(IProductsService productsService)
		{
			this.productsService = productsService;
		}

		public IActionResult All()
		{
			var products = productsService.GetAll();
			return this.View(products);
		}

		public IActionResult Create()
		{
			return this.View();
		}

		[HttpPost]
		public IActionResult Create(ProductCreateInputModel model)
		{
			if(!ModelState.IsValid)
			{
				return Redirect("/Products/Create");
			}

			productsService.Create(model.MapTo<Product>());

			return this.Redirect("/Products/All");
		}
	}
}
