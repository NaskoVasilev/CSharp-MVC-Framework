using Musaca.Data;
using Musaca.Services;
using MvcFramework;
using MvcFramework.DependencyContainer;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;

namespace Musaca.Web
{
	public class Startup : IMvcApplication
	{
		public void Configure(IServerRoutingTable serverRoutingTable, RouteSettings routeSettings)
		{
			using (var context = new MusacaDbContext())
			{
				context.Database.EnsureCreated();
			}
		}

		public void ConfigureServices(IServiceProvider serviceProvider)
		{
			serviceProvider.Add<IUsersService, UsersService>();
			serviceProvider.Add<IProductsService, ProductsService>();
			serviceProvider.Add<IOrdersService, OrdersService>();
		}
	}
}
