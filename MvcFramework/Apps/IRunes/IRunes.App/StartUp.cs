using IRunes.App.Services;
using IRunes.Data;
using IRunes.Services;
using MvcFramework;
using MvcFramework.DependencyContainer;
using MvcFramework.HTTP.Enums;
using MvcFramework.Results;
using MvcFramework.Routing.Contracts;

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

			serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new RedirectResult("/Home/Index"));
		}

		public void ConfigureServices(IServiceProvider serviceProvider)
		{
			serviceProvider.Add<IUserService, UserService>();
			serviceProvider.Add<IAlbumService, AlbumService>();
			serviceProvider.Add<ITrackService, TrackService>();
			serviceProvider.Add<IPasswordService, PasswordService>();
		}
	}
}
