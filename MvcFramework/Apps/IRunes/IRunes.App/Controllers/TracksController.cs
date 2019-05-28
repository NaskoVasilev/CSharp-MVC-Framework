using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using MvcFramework;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
	public class TracksController : Controller
	{
		public IHttpResponse Create(IHttpRequest request)
		{
			if (!this.IsLogedIn(request))
			{
				Redirect("/Users/Login");
			}

			ViewData["albumId"] = request.QueryData["albumId"].ToString();
			return View();
		}

		public IHttpResponse CreateConfirm(IHttpRequest request)
		{
			if (!this.IsLogedIn(request))
			{
				Redirect("/Users/Login");
			}

			string albumId = request.QueryData["albumId"].ToString();
			string name = request.FormData["name"].ToString();
			string link = request.FormData["link"].ToString();
			decimal price = decimal.Parse(request.FormData["price"].ToString());

			using (var context = new RunesDbContext())
			{
				Album album = context.Albums.Find(albumId);
				if (album == null)
				{
					return Redirect("/Tracks/Create?albumId=" + albumId);
				}

				Track track = new Track { Name = name, Link = link, Price = price, AlbumId = albumId };
				album.Price += price * GlobalConstants.PriceDiscountConeficient;
				context.Tracks.Add(track);
				context.SaveChanges();
			}

			return Redirect("/Albums/Details?id=" + albumId);
		}

		public IHttpResponse Details(IHttpRequest request)
		{
			if (!this.IsLogedIn(request))
			{
				Redirect("/Users/Login");
			}

			string id = request.QueryData["id"].ToString();

			using (var context = new RunesDbContext())
			{
				Track track = context.Tracks.Find(id);
				ViewData["track"] = track.ToTrackDetialsHtml();
				ViewData["link"] = track.Link;
				ViewData["albumId"] = track.AlbumId;
			}

			return View();
		}
	}
}
