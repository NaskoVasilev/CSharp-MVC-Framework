using IRunes.App.ViewModels;
using MvcFramework;
using MvcFramework.Results;

namespace IRunes.App.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			if (this.IsLogedIn())
			{
				UserHomeViewModel model = new UserHomeViewModel { Username = this.User.Username };
				return View(model, "Index-Logged");
			}

			return View();
		}
	}
}
