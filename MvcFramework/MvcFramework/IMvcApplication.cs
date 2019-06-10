using MvcFramework.DependencyContainer;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;

namespace MvcFramework
{
	public interface IMvcApplication
	{
		void Configure(IServerRoutingTable serverRoutingTable, RouteSettings routeSettings);

		void ConfigureServices(IServiceProvider serviceProvider);
	}
}
