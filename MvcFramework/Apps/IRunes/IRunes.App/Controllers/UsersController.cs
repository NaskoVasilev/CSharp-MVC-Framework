using IRunes.App.Services;
using IRunes.Data;
using IRunes.Models;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Results;
using System.Linq;

namespace IRunes.App.Controllers
{
	public class UsersController : Controller
	{
		private readonly IPasswordService passwordService;

		public UsersController()
		{
			this.passwordService = new PasswordService();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost(ActionName = nameof(Login))]
		public IActionResult LoginConfirm()
		{
			string username = Request.FormData["username"].ToString();
			string password = Request.FormData["password"].ToString();
			string hashedPassword = passwordService.HashPassword(password);

			using (var context = new RunesDbContext())
			{
				User user = context.Users.FirstOrDefault(u => (u.Username == username ||
				u.Email == username) && u.Password == hashedPassword);

				if (user == null)
				{
					return Redirect("/Users/Login");
				}

				this.SignIn(user.Id, user.Username, user.Email);
			}

			return this.Redirect("/");
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost(ActionName = nameof(Register))]
		public IActionResult RegisterConfirm()
		{
			string username = Request.FormData["username"].ToString();
			string email = Request.FormData["email"].ToString();
			string password = Request.FormData["password"].ToString();
			string confirmPassword = Request.FormData["confirmPassword"].ToString();

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
				SignIn(user.Id, user.Username, user.Email);
			}

			return this.Redirect("/");
		}

		public IActionResult Logout()
		{
			SignOut();
			return Redirect("/");
		}
	}
}
