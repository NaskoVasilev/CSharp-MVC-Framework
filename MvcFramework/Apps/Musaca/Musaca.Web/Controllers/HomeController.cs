using Musaca.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Results;

namespace Musaca.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IOrdersService ordersService;

		public HomeController(IOrdersService ordersService)
		{
			this.ordersService = ordersService;
		}

		[HttpGet(Url = "/")]
		public IActionResult IndexSlash()
		{
			return Index();
		}

		public IActionResult Index()
		{
			if(this.IsLogedIn())
			{
				var currentActiveOrder = ordersService.GetUserActiveOrder(this.User.Id);
				return View(currentActiveOrder, "IndexLoggedIn");
			}

			return View();
		}
	}
}
