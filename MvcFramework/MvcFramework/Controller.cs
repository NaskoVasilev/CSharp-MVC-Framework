using MvcFramework.Common;
using MvcFramework.Extensions;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.Identity;
using MvcFramework.Results;
using MvcFramework.Validation;
using MvcFramework.ViewEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MvcFramework
{
	public abstract class Controller
	{
		private IViewEngine viewEngine;

		public Controller()
		{
			this.ModelState = new ModelStateDictionary();
			this.viewEngine = new SisViewEngine();
		}

		protected Principal User
		{
			get
			{
				if (this.Request.Session.ContainsParameter("principal"))
				{
					return (Principal)Request.Session.GetParameter("principal");
				}

				return null;
			}
		}

		protected ModelStateDictionary ModelState { get; private set; }

		protected IHttpRequest Request { get; private set; }

		protected IActionResult View([CallerMemberName] string viewName = null)
		{
			return this.View<object>(null, viewName);
		}

		protected IActionResult View<T>(T model = null, [CallerMemberName] string viewName = null) where T : class
		{
			string controllerName = this.GetType().Name.Replace("Controller", "");
			string path = $"Views/{controllerName}/{viewName}.html";
			string viewContent = System.IO.File.ReadAllText(path);
			string layoutView = System.IO.File.ReadAllText($"Views/{GlobalConstants.LayoutName}");
			string view = layoutView.Replace("@RenderBody()", viewContent);

			//TODO: use ParseTemplate method to replace ViewData keyValuePairs
			view = viewEngine.GetHtml<T>(view, model, this.User);

			return new HtmlResult(view, HttpResponseStatusCode.Ok);
		}

		protected IActionResult Redirect(string url)
		{
			return new RedirectResult(url);
		}

		protected bool IsLogedIn()
		{
			return this.User != null;
		}

		protected void SignIn(string id, string username, string email)
		{
			Principal principal = new Principal(id, username, email);
			Request.Session.AddParameter("principal", principal);
		}

		protected void SignOut()
		{
			Request.Session.ClearParameters();
		}

		protected IActionResult Xml(object obj)
		{
			string xmlContent = obj.ToXml();
			return new XmlResult(xmlContent);
		}

		protected IActionResult Xml(object obj, string rootName)
		{
			string xmlContent = obj.ToXml(rootName);
			return new XmlResult(xmlContent);
		}

		protected IActionResult Json(object obj)
		{
			string jsonContent = obj.ToJson();
			return new JsonResult(jsonContent);
		}

		protected IActionResult File(byte[] fileContent, string fileName = "file.txt")
		{
			return new FileResult(fileContent, fileName);
		}

		protected IActionResult NotFound(string message = GlobalConstants.NotFoundDefaultMessage)
		{
			return new NotFoundResult(message);
		}
	}
}
