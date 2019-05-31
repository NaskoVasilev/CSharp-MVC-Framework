using IRunes.Data;
using MvcFramework;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.Results;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.App.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index(IHttpRequest httpRequest)
		{
			if (this.IsLogedIn(httpRequest))
			{
				ViewData["username"] = GetUsername(httpRequest);
				return View("Index-Logged");
			}

			return View();
		}
	}
}
