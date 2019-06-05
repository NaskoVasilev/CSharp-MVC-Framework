using IRunes.App.Services;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Results;
using System.Linq;

namespace IRunes.App.Controllers
{
	public class UsersController : Controller
	{
		private readonly IPasswordService passwordService;
		private readonly IUserService userService;

		public UsersController(IPasswordService passwordService, IUserService userService)
		{
			this.passwordService = passwordService;
			this.userService = userService;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost(ActionName = nameof(Login))]
		public IActionResult LoginConfirm(string username, string password)
		{
			string hashedPassword = passwordService.HashPassword(password);

			User user = userService.GetUserByUsernameAndPassword(username, hashedPassword);

			if (user == null)
			{
				return Redirect("/Users/Login");
			}

			this.SignIn(user.Id, user.Username, user.Email);

			return this.Redirect("/");
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost(ActionName = nameof(Register))]
		public IActionResult RegisterConfirm(string username, string email, string password, string confirmPassword)
		{
			if (password != confirmPassword)
			{
				return Redirect("/Users/Register");
			}

			if (userService.GetUserByUsernameOrEmail(username, email) != null)
			{
				return Redirect("/Users/Register");
			}

			User user = new User
			{
				Username = username,
				Email = email,
				Password = passwordService.HashPassword(password)
			};

			userService.CreateUser(user);
			SignIn(user.Id, user.Username, user.Email);

			return this.Redirect("/");
		}

		public IActionResult Logout()
		{
			SignOut();
			return Redirect("/");
		}
	}
}
