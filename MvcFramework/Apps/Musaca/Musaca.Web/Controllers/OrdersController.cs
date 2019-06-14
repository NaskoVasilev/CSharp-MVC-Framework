using Musaca.Models.Orders;
using Musaca.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;

namespace Musaca.Web.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly IOrdersService ordersService;
		private readonly IProductsService productsService;

		public OrdersController(IOrdersService ordersService, IProductsService productsService)
		{
			this.ordersService = ordersService;
			this.productsService = productsService;
		}

		public IActionResult Cashout(string id)
		{
			ordersService.Cashout(id, this.User.Id);
			return Redirect("/");
		}

		[HttpPost]
		public IActionResult Order(OrderInputModel model)
		{
			if(!ModelState.IsValid)
			{
				return Redirect("/");
			}

			string productId = productsService.GetProductByName(model.Product);
			if(productId == null)
			{
				return Redirect("/");
			}

			ordersService.AddProductToOrder(model.OrderId, productId);
			return Redirect("/");
		}
	}
}
