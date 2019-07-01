using MvcFramework;
using MvcFramework.DependencyContainer;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;
using SULS.Data;
using SULS.Services;

namespace SULS.App
{
	public class StartUp : IMvcApplication
    {
		public void Configure(IServerRoutingTable serverRoutingTable, RouteSettings routeSettings)
		{
			using (var db = new SULSContext())
			{
				db.Database.EnsureCreated();
			}
		}

		public void ConfigureServices(IServiceProvider serviceProvider)
        {
			serviceProvider.Add<IUsersService, UsersService>();
			serviceProvider.Add<ISubmissionsService, SubmissionsService>();
			serviceProvider.Add<IProblemsService, ProblemsService>();
		}
    }
}