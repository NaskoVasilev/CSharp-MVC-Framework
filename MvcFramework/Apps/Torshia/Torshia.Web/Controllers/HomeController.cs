using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Results;
using System.Linq;
using Torshia.Services;

namespace Musaca.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ITasksService tasksService;

		public HomeController(ITasksService tasksService)
		{
			this.tasksService = tasksService;
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
				var tasks = tasksService.GetAllTasks().ToList();
				return View(tasks, "IndexLoggedIn");
			}

			return View();
		}
	}
}
