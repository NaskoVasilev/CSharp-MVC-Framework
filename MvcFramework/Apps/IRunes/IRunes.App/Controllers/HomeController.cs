using MvcFramework;
using MvcFramework.Results;

namespace IRunes.App.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
