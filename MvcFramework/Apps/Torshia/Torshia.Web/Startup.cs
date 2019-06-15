using MvcFramework;
using MvcFramework.DependencyContainer;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;
using Torshia.Data;
using Torshia.Services;

namespace Torshia.Web
{
	public class Startup : IMvcApplication
	{
		public void Configure(IServerRoutingTable serverRoutingTable, RouteSettings routeSettings)
		{
			using (var context = new TorshiaDbContext())
			{
				bool isCreated = context.Database.EnsureCreated();
				if (isCreated)
				{
					Seeder.SeedRoles();
					Seeder.SeedAdmin();
				}
			}
		}

		public void ConfigureServices(IServiceProvider serviceProvider)
		{
			serviceProvider.Add<IUsersService, UsersService>();
			serviceProvider.Add<ITasksService, TasksService>();
			serviceProvider.Add<IReportsService, ReportsService>();
		}
	}
}
