using MvcFramework;
using MvcFramework.DependencyContainer;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;
using Panda.Data;
using Panda.Services;

namespace Panda.Web
{
	public class Startup : IMvcApplication
	{
		public void Configure(IServerRoutingTable serverRoutingTable, RouteSettings routeSettings)
		{
			using (var context = new PandaDbContext())
			{
				context.Database.EnsureCreated();
			}
		}

		public void ConfigureServices(IServiceProvider serviceProvider)
		{
			serviceProvider.Add<IUsersService, UsersService>();
			serviceProvider.Add<IPackagesService, PackagesService>();
			serviceProvider.Add<IReceiptsService, ReceiptsService>();
		}
	}
}
