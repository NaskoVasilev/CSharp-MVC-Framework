using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.WebServer.Results;

namespace MvcFramework.Demo.Controllers
{
	public class HomeController : BaseController
	{
		public IHttpResponse Index(IHttpRequest request)
		{
			return View();
		}

		public IHttpResponse Welcome(IHttpRequest request)
		{
			if (this.IsLogedIn(request))
			{
				string username = request.Session.GetParameter("username").ToString();
				ViewData["username"] = username;
			}

			return View();
		}

		public IHttpResponse About()
		{
			string content = "<h1>Hello, World!</h1><h2>This is custom http server like IIS and we are building custom MVC Framework like ASP.NET Core</h2>";
			return new HtmlResult(content, HttpResponseStatusCode.Ok);
		}

		public IHttpResponse GreetUser(IHttpRequest request)
		{
			string content = $"Your name is {request.QueryData["name"]} and you are {request.QueryData["age"]} years old";
			return new TextResult(content, HttpResponseStatusCode.Ok);
		}
	}
}
