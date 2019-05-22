using MvcFramework.Demo.Controllers;
using MvcFramework.HTTP.Enums;
using MvcFramework.WebServer;
using MvcFramework.WebServer.Routing;
using MvcFramework.WebServer.Routing.Contracts;
using System;
using System.Net.Sockets;

namespace MvcFramework.Demo
{
	public class StartUp
	{
		private const int Port = 8000;

		public static void Main(string[] args)
		{
			IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

			//Http get methods
			serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequset => new HomeController().Index(httpRequset));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/about", httpRequset => new HomeController().About());
			serverRoutingTable.Add(HttpRequestMethod.Get, "/greet", httpRequset => new HomeController().GreetUser(httpRequset));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/user/login", httpRequset => new UserController().Login());
			serverRoutingTable.Add(HttpRequestMethod.Get, "/user/register", httpRequset => new UserController().Register());
			serverRoutingTable.Add(HttpRequestMethod.Get, "/user/logout", httpRequset => new UserController().Logout(httpRequset));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/welcome", httpRequset => new HomeController().Welcome(httpRequset));

			//Http post methods
			serverRoutingTable.Add(HttpRequestMethod.Post, "/user/login", httpRequset => new UserController().Login(httpRequset));
			serverRoutingTable.Add(HttpRequestMethod.Post, "/user/register", httpRequset => new UserController().Register(httpRequset));

			Server server = new Server(Port, serverRoutingTable);
			server.Run();
		}
	}
}
