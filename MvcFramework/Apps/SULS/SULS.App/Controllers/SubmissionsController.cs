using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using SULS.InputModels.Submissions;
using SULS.Models;
using SULS.Services;
using SULS.ViewModels.Problems;

namespace SULS.App.Controllers
{
	[Authorize]
	public class SubmissionsController : Controller
	{
		private readonly ISubmissionsService submissionsService;
		private readonly IProblemsService problemsService;

		public SubmissionsController(ISubmissionsService submissionsService, IProblemsService problemsService)
		{
			this.submissionsService = submissionsService;
			this.problemsService = problemsService;
		}

		public IActionResult Create(string id)
		{
			ProblemSubmissionViewModel model = problemsService.GetProblemInfo(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult Create(SubmissionCreateInputModel model)
		{
			if(!ModelState.IsValid)
			{
				return Redirect("/Submissions/Create?id=" + model.ProblemId);
			}

			Submission submission = new Submission
			{
				Code = model.Code,
				ProblemId = model.ProblemId,
				UserId = this.User.Id,
			};

			submissionsService.Create(submission);

			return Redirect("/");
		}

		public IActionResult Delete(string id)
		{
			submissionsService.Delete(id);
			return Redirect("/");
		}
	}
}
