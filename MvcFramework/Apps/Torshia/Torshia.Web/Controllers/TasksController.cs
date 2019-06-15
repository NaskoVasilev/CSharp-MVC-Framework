using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Torshia.Models.Tasks;
using Torshia.Services;

namespace Torshia.Web.Controllers
{
	public class TasksController : Controller
	{
		private readonly ITasksService tasksService;

		public TasksController(ITasksService tasksService)
		{
			this.tasksService = tasksService;
		}

		[Authorize(Constants.RoleAdminName)]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]

		[Authorize(Constants.RoleAdminName)]
		public IActionResult Create(TaskCreateInputModel model)
		{
			if (!ModelState.IsValid)
			{
				return Redirect("/Tasks/Create");
			}

			if(model.AffectedSectors.Count == 0)
			{
				ModelState.Add(nameof(model.AffectedSectors), "The affcetd sectors must be at least one!");
				return Redirect("/Tasks/Create");
			}

			tasksService.Create(model);

			return Redirect("/");
		}

		[Authorize]
		public IActionResult Details(string id)
		{
			TaskViewModel model = tasksService.GetById(id);
			return View(model);
		}

		[Authorize]
		public IActionResult Report(string id)
		{
			tasksService.Report(id, User.Id);
			return Redirect("/");
		}
	}
}
