using IRunes.App.Services;
using IRunes.App.ViewModels;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Results;

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

		[HttpPost]
		public IActionResult Login(UserLoginInputModel model)
		{
			if(!ModelState.IsValid)
			{
				return Redirect("/Users/Login");
			}

			string hashedPassword = passwordService.HashPassword(model.Password);

			User user = userService.GetUserByUsernameAndPassword(model.Username, hashedPassword);

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

		[HttpPost]
		public IActionResult Register(UserRegisterInputModel model)
		{
			if (!ModelState.IsValid)
			{
				return Redirect("/Users/Register");
			}

			if (model.Password != model.ConfirmPassword)
			{
				ModelState.Add(string.Empty, "The passwords must match");
				return Redirect("/Users/Register");
			}

			if (userService.GetUserByUsernameOrEmail(model.Username, model.Email) != null)
			{
				return Redirect("/Users/Register");
			}

			User user = new User
			{
				Username = model.Username,
				Email = model.Email,
				Password = passwordService.HashPassword(model.Password)
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
