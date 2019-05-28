using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;

namespace MvcFramework
{
	public static class WebHost
	{
		public static void Start(IMvcApplication application)
		{
			IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

			application.Configure(serverRoutingTable);
		}
	}
}
