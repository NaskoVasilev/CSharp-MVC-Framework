using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.AutoMapper.Extensions;
using MvcFramework.Results;
using System.Collections.Generic;
using Torshia.Data.Models;
using Torshia.Models.Users;
using Torshia.Services;

namespace Torshia.Web.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUsersService usersService;

		public UsersController(IUsersService usersService)
		{
			this.usersService = usersService;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginInputModel model)
		{
			User user = usersService.GetUserOrNull(model.Username, model.Password);
			if (user == null)
			{
				ModelState.Add("Username", "Invalid username or password!");
				return Redirect("/Users/Login");
			}

			var roles = usersService.GetUserRoles(user.Id);
			SignIn(user.Id, user.Username, user.Email, roles);
			return Redirect("/");
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterInputModel model)
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

			if (usersService.UserExists(model.Username))
			{
				ModelState.Add(string.Empty, "User with the same username already exists!");
				return Redirect("/Users/Register");
			}

			User user = model.MapTo<User>();
			string userId = usersService.CreateUser(user);

			usersService.AddToRole(Constants.RoleUserName, userId);
			var roles = usersService.GetUserRoles(userId);
			SignIn(userId, model.Username, model.Email, roles);

			return Redirect("/");
		}

		public IActionResult Logout()
		{
			SignOut();
			return Redirect("/");
		}
	}
}
