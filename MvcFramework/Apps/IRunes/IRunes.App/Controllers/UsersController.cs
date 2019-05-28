using IRunes.App.Services;
using IRunes.Data;
using IRunes.Models;
using MvcFramework;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using System.Linq;

namespace IRunes.App.Controllers
{
	public class UsersController : Controller
	{
		private readonly IPasswordService passwordService;
		private readonly UserManager userManager;

		public UsersController()
		{
			this.passwordService = new PasswordService();
			this.userManager = new UserManager();
		}

		public IHttpResponse Login()
		{
			return View();
		}

		public IHttpResponse Login(IHttpRequest httpRequest)
		{
			string username = httpRequest.FormData["username"].ToString();
			string password = httpRequest.FormData["password"].ToString();
			string hashedPassword = passwordService.HashPassword(password);

			using (var context = new RunesDbContext())
			{
				User user = context.Users.FirstOrDefault(u => (u.Username == username ||
				u.Email == username) && u.Password == hashedPassword);

				if (user == null)
				{
					return Redirect("/Users/Login");
				}

				userManager.SignIn(httpRequest, user);
			}

			return this.Redirect("/");
		}

		public IHttpResponse Register()
		{
			return View();
		}

		public IHttpResponse Register(IHttpRequest httpRequest)
		{
			string username = httpRequest.FormData["username"].ToString();
			string email = httpRequest.FormData["email"].ToString();
			string password = httpRequest.FormData["password"].ToString();
			string confirmPassword = httpRequest.FormData["confirmPassword"].ToString();

			if (password != confirmPassword)
			{
				return Redirect("/Users/Register");
			}

			using (var context = new RunesDbContext())
			{
				if (context.Users.Any(u => u.Username == username || u.Email == email))
				{
					return Redirect("/Users/Register");
				}

				User user = new User
				{
					Username = username,
					Email = email,
					Password = passwordService.HashPassword(password)
				};

				context.Users.Add(user);
				context.SaveChanges();
				userManager.SignIn(httpRequest, user);
			}

			return this.Redirect("/");
		}

		public IHttpResponse Logout(IHttpRequest request)
		{
			userManager.SignOut(request);
			return Redirect("/");
		}
	}
}
