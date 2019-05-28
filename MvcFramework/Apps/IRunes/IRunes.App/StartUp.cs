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

			serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new RedirectResult("/Home/Index"));
		}

		public void ConfigureServices()
		{
			throw new NotImplementedException();
		}
	}
}
