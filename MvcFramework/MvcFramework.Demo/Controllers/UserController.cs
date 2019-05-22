using MvcFramework.Data;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.Models;
using System.Linq;

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

			using(var context = new DemoDbContext())
			{
				User user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

				if(user == null)
				{
					return Redirect("/user/login");
				}
			}

			httpRequest.Session.AddParameter("username", username);

			return this.Redirect("/welcome");
		}

		public IHttpResponse Register()
		{
			return View();
		}

		public IHttpResponse Register(IHttpRequest httpRequest)
		{
			string username = httpRequest.FormData["username"].ToString();
			string password = httpRequest.FormData["password"].ToString();
			string confirmPassword = httpRequest.FormData["confirmPassword"].ToString();

			if(password != confirmPassword)
			{
				return Redirect("/user/register");
			}

			using(var context = new DemoDbContext())
			{
				if(context.Users.Any(u => u.Username == username))
				{
					return Redirect("/user/register");
				}

				//TODO: hash the password
				User user = new User
				{
					Username = username,
					Password = password
				};

				context.Users.Add(user);
				context.SaveChanges();
			}

			return this.Redirect("/user/login");
		}

		public IHttpResponse Logout(IHttpRequest request)
		{
			request.Session.ClearParameters();
			return Redirect("/");
		}
	}
}
