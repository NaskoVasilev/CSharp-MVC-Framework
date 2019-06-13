using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.AutoMapper.Extensions;
using MvcFramework.Results;
using Panda.Data.Models;
using Panda.Data.Models.Enums;
using Panda.Services;
using Panda.Web.ViewModels.Packages;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Web.Controllers
{
	[Authorize]
	public class PackagesController : Controller
	{
		private readonly IPackagesService packagesService;
		private readonly IUsersService usersService;

		public PackagesController(IPackagesService packagesService, IUsersService usersService)
		{
			this.packagesService = packagesService;
			this.usersService = usersService;
		}

		public IActionResult Create()
		{
			var usernames = usersService.GetUsernames();
			return this.View(usernames);
		}

		[HttpPost]
		public IActionResult Create(PackageCreateInputModel model)
		{
			if (!ModelState.IsValid)
			{
				return Redirect("/Packages/Create");
			}

			Package package = model.MapTo<Package>();
			string userId = usersService.GetUserIdByUsername(model.RecipientName);
			if(userId == null)
			{
				ModelState.Add("Recipient", "Recipient is invalid!");
				return Redirect("/Packages/Create");
			}
			package.RecipientId = userId;

			packagesService.CreatePackage(package);

			return Redirect("/Packages/Pending");
		}

		public IActionResult Pending()
		{
			List<PackageViewModel> packages = packagesService.GetByStatus(PackageStatus.Pending, User.Username)
				.Select(p => new PackageViewModel()
				{
					Id = p.Id,
					Description = p.Description,
					Weight = p.Weight,
					RecipientName = p.Recipient.Username,
					ShippingAddress = p.ShippingAddress
				})
				.ToList();

			return View(packages);
		}

		public IActionResult Delivered()
		{
			List<PackageViewModel> packages = packagesService.GetByStatus(PackageStatus.Delivered, User.Username)
				.Select(p => new PackageViewModel()
				{
					Id = p.Id,
					Description = p.Description,
					Weight = p.Weight,
					RecipientName = p.Recipient.Username,
					ShippingAddress = p.ShippingAddress
				})
				.ToList();

			return View(packages);
		}

		public IActionResult Deliver(string id)
		{
			packagesService.DeliverPackage(id);
			return Redirect("/Packages/Delivered");
		}
	}
}
