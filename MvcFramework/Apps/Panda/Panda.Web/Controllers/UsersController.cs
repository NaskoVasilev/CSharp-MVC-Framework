using MvcFramework;
using MvcFramework.Results;

namespace Panda.Web.Controllers
{
	public class UsersController : Controller
	{
		public IActionResult Login()
		{
			return this.View();
		}

		public IActionResult Register()
		{
			return this.View();
		}
	}
}
