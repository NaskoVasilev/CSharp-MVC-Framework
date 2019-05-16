using MvcFramework.Demo.Controllers;
using MvcFramework.HTTP.Enums;
using MvcFramework.WebServer;
using MvcFramework.WebServer.Routing;
using MvcFramework.WebServer.Routing.Contracts;
using System;

namespace MvcFramework.Demo
{
	public class StartUp
	{
		private const int Port = 7777;

		public static void Main(string[] args)
		{
			IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

			serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequset => new HomeController().Index());
			serverRoutingTable.Add(HttpRequestMethod.Get, "/about", httpRequset => new HomeController().About());
			serverRoutingTable.Add(HttpRequestMethod.Get, "/greet", httpRequset => new HomeController().GreetUser(httpRequset));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/login", httpRequset => new UserController().Login());
			serverRoutingTable.Add(HttpRequestMethod.Post, "/login", httpRequset => new UserController().Login(httpRequset));

			Server server = new Server(Port, serverRoutingTable);
			server.Run();
		}
	}
}
