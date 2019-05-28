using IRunes.App.Controllers;
using IRunes.Data;
using MvcFramework;
using MvcFramework.HTTP.Enums;
using MvcFramework.Results;
using MvcFramework.Routing.Contracts;
using System;

namespace IRunes.App
{
	public class Startup : IMvcApplication
	{
		public void Configure(IServerRoutingTable serverRoutingTable)
		{
			using (var context = new RunesDbContext())
			{
				context.Database.EnsureCreated();
			}

			#region Home Routes
			serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new RedirectResult("/Home/Index"));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Home/Index", request => new HomeController().Index(request));
			#endregion

			#region User Routes
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Login", request => new UsersController().Login());
			serverRoutingTable.Add(HttpRequestMethod.Post, "/Users/Login", request => new UsersController().Login(request));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Register", request => new UsersController().Register());
			serverRoutingTable.Add(HttpRequestMethod.Post, "/Users/Register", request => new UsersController().Register(request));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Users/Logout", request => new UsersController().Logout(request));
			#endregion

			#region Album Routes
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/All", request => new AlbumsController().All(request));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/Create", request => new AlbumsController().Create(request));
			serverRoutingTable.Add(HttpRequestMethod.Post, "/Albums/Create", request => new AlbumsController().CreateConfirm(request));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Albums/Details", request => new AlbumsController().Details(request));
			#endregion

			#region Track Routes
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Tracks/Create", request => new TracksController().Create(request));
			serverRoutingTable.Add(HttpRequestMethod.Post, "/Tracks/Create", request => new TracksController().CreateConfirm(request));
			serverRoutingTable.Add(HttpRequestMethod.Get, "/Tracks/Details", request => new TracksController().Details(request));
			#endregion

			Server server = new Server(8000, serverRoutingTable);
			server.Run();
		}

		public void ConfigureServices()
		{
			throw new NotImplementedException();
		}
	}
}
