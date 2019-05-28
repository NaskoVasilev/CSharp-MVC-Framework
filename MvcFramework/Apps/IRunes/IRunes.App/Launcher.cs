namespace IRunes.App
{
	using Data;
	using IRunes.App.Controllers;
	using MvcFramework.HTTP.Enums;
	using MvcFramework.WebServer;
	using MvcFramework.WebServer.Results;
	using MvcFramework.WebServer.Routing;

	public class Launcher
    {
        public static void Main(string[] args)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }

            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            Configure(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void Configure(ServerRoutingTable serverRoutingTable)
        {
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
		}
	}
}
