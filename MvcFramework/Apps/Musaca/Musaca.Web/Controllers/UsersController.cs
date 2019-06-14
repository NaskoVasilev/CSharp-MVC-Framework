using Musaca.Data.Models;
using Musaca.Models.Users;
using Musaca.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.AutoMapper.Extensions;
using MvcFramework.Results;

namespace Musaca.Web.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUsersService usersService;
		private readonly IOrdersService ordersService;

		public UsersController(IUsersService usersService, IOrdersService ordersService)
		{
			this.usersService = usersService;
			this.ordersService = ordersService;
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

			this.SignIn(user.Id, user.Username, user.Email);
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
			this.SignIn(userId, model.Username, model.Email);

			return Redirect("/");
		}

		public IActionResult Logout()
		{
			SignOut();
			return Redirect("/");
		}

		[Authorize]
		public IActionResult Profile()
		{
			var ordres = ordersService.GetCashierCompletedOrders(this.User.Id);
			return View(ordres);
		}
	}
}
