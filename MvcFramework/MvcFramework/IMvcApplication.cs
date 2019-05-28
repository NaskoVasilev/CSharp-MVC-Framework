using MvcFramework.Routing.Contracts;

namespace MvcFramework
{
	public interface IMvcApplication
	{
		void Configure(IServerRoutingTable serverRoutingTable);

		void ConfigureServices();
	}
}
