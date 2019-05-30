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

		public IActionResult JsonArray(IHttpRequest httpRequest)
		{
			using (var db = new RunesDbContext())
			{
				return Json(db.Albums.ToList());
			}
		}

		public IActionResult Json(IHttpRequest httpRequest)
		{
			using (var db = new RunesDbContext())
			{
				return Json(db.Albums.FirstOrDefault());
			}
		}

		public IActionResult Xml(IHttpRequest httpRequest)
		{
			return Xml(new Test("Atanas", 18, true), "SomeTest");
		}

		public IActionResult XmlArray(IHttpRequest httpRequest)
		{
			var tests = new List<Test>()
			{
				new Test("Atanas", 18, true),
				new Test("Atana", 20, false),
				new Test("Atan", 15, true),
				new Test("Ata", 15, false),
				new Test("At", 12, true),

			};
			return Xml(tests, "Tests");
		}
	}
}
