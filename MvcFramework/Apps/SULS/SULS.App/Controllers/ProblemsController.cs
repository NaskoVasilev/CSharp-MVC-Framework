using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using SULS.InputModels.Problems;
using SULS.Models;
using SULS.Services;
using SULS.ViewModels.Problems;

namespace SULS.App.Controllers
{
	[Authorize]
	public class ProblemsController : Controller
	{
		private readonly IProblemsService problemsService;

		public ProblemsController(IProblemsService problemsService)
		{
			this.problemsService = problemsService;
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(ProblemCreateInputModel model)
		{
			if(!ModelState.IsValid)
			{
				return Redirect("/Problems/Create");
			}

			Problem problem = new Problem { Name = model.Name, Points = model.Points };
			problemsService.Create(problem);

			return Redirect("/");
		}

		[Authorize]
		public IActionResult Details(string id)
		{
			ProblemDetailsViewModel model = problemsService.GetProblemDetails(id);
			return View(model);
		}
	}
}
