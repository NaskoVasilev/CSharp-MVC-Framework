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
				ViewData["username"] = User.Username;
				return View("Index-Logged");
			}

			return View();
		}
	}
}
