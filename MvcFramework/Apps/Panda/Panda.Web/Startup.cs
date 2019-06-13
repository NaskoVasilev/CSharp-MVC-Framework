using MvcFramework;
using MvcFramework.DependencyContainer;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;
using Panda.Data;

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

		}
	}
}
