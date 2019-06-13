using System.Linq;
using Panda.Data;
using Panda.Data.Models;
using Panda.Data.Models.Enums;

namespace Panda.Services
{
	public class PackagesService : IPackagesService
	{
		private readonly PandaDbContext context;
		private readonly IReceiptsService receiptsService;
		private readonly IUsersService usersService;

		public PackagesService(PandaDbContext context, IReceiptsService receiptsService, IUsersService usersService)
		{
			this.context = context;
			this.receiptsService = receiptsService;
			this.usersService = usersService;
		}

		public void CreatePackage(Package package)
		{
			package.Status = PackageStatus.Pending;
			context.Packages.Add(package);
			context.SaveChanges();
		}

		public void DeliverPackage(string id)
		{
			Package package = context.Packages.Find(id);
			if(package == null)
			{
				return;
			}

			package.Status = PackageStatus.Delivered;
			context.SaveChanges();
			receiptsService.CreateReceipt(package);
		}

		public IQueryable<Package> GetByStatus(PackageStatus status, string username)
		{
			string userId = usersService.GetUserIdByUsername(username);
			var packages = context.Packages.Where(p => p.Status == status && p.RecipientId == userId);
			return packages;
		}
	}
}
