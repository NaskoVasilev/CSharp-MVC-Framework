using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
	public class HomeController : BaseController
	{
		public IHttpResponse Index(IHttpRequest httpRequest)
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
