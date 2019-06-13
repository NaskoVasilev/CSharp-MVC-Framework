using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Results;

namespace Panda.Web.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet(Url = "/")]
		public IActionResult IndexSlash()
		{
			return this.Index();
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
