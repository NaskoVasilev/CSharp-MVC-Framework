using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.WebServer.Results;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace IRunes.App.Controllers
{
	public abstract class BaseController
	{
		protected Dictionary<string, object> ViewData { get; set; }

		public BaseController()
		{
			this.ViewData = new Dictionary<string, object>();
		}

		protected IHttpResponse View([CallerMemberName] string view = null)
		{
			string controllerName = this.GetType().Name.Replace("Controller", "");
			string path = $"Views/{controllerName}/{view}.html";
			string viewContent = File.ReadAllText(path);
			viewContent = ParseTemplate(viewContent);

			return new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
		}

		protected IHttpResponse Redirect(string url)
		{
			return new RedirectResult(url);
		}

		protected bool IsLogedIn(IHttpRequest request)
		{
			if (request.Session.ContainsParameter("username"))
			{
				return true;
			}
			return false;
		}

		protected string GetUsername(IHttpRequest request)
		{
			if (this.IsLogedIn(request))
			{
				return request.Session.GetParameter("username").ToString();
			}

			return null;
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
