using MvcFramework.Extensions;
using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.Identity;
using MvcFramework.Results;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MvcFramework
{
	public abstract class Controller
	{
		public Controller()
		{
			ViewData = new Dictionary<string, object>();
		}

		protected Dictionary<string, object> ViewData { get; }

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


		protected IHttpRequest Request { get; private set; }

		protected IActionResult View([CallerMemberName] string view = null)
		{
			string controllerName = GetType().Name.Replace("Controller", "");
			string path = $"Views/{controllerName}/{view}.html";
			string viewContent = System.IO.File.ReadAllText(path);
			viewContent = ParseTemplate(viewContent);

			return new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
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

		private string ParseTemplate(string template)
		{
			foreach (var data in ViewData)
			{
				template = template.Replace($"@{data.Key}", data.Value.ToString());
			}

			return template;
		}
	}
}
