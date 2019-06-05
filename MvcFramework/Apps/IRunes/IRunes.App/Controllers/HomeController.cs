using IRunes.App.ViewModels;
using MvcFramework;
using MvcFramework.Logging;
using MvcFramework.Results;
using System.Collections.Generic;

namespace IRunes.App.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger logger;

		public HomeController(ILogger logger)
		{
			this.logger = logger;
		}

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
