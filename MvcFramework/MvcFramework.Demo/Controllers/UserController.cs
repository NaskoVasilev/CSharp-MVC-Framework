using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.WebServer.Results;

namespace MvcFramework.Demo.Controllers
{
	public class UserController : BaseController
	{ 
		public IHttpResponse Login()
		{
			return View();
		}

		public IHttpResponse Login(IHttpRequest httpRequest)
		{
			string username = httpRequest.FormData["username"].ToString();
			string password = httpRequest.FormData["password"].ToString();
			string content = $"Your username is {username} and your password is {password}";
			return new TextResult(content, HttpResponseStatusCode.Ok);
		}
	}
}
