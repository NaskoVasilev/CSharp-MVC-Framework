using MvcFramework;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using Torshia.Models.Reports;
using Torshia.Services;

namespace Torshia.Web.Controllers
{
	public class ReportsController : Controller
	{
		private readonly IReportsService reportsService;

		public ReportsController(IReportsService reportsService)
		{
			this.reportsService = reportsService;
		}

		[Authorize(Constants.RoleAdminName)]
		public IActionResult All()
		{
			var reports = reportsService.GetAll();
			return View(reports);
		}

		[Authorize(Constants.RoleAdminName)]
		public IActionResult Details(string id)
		{
			ReportDetailsViewModel report = reportsService.GetById(id);
			return View(report);
		}
	}
}
