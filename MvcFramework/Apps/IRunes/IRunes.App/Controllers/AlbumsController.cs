using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using MvcFramework;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using System.Linq;

namespace IRunes.App.Controllers
{
	public class AlbumsController : Controller
	{
		public IHttpResponse All(IHttpRequest request)
		{
			if (!this.IsLogedIn(request))
			{
				return Redirect("/Users/Login");
			}

			using (var context = new RunesDbContext())
			{
				var albums = context.Albums
					.ToList()
					.Select(a => a.ToHtml())
					.ToList();

				if(albums.Count == 0)
				{
					ViewData["albums"] = "There are currently no albums.";
				}
				else
				{
					ViewData["albums"] = string.Join("", albums);
				}
			}

			return View();
		}

		public IHttpResponse Create(IHttpRequest request)
		{
			if (!this.IsLogedIn(request))
			{
				return Redirect("/Users/Login");
			}

			return View();
		}

		public IHttpResponse CreateConfirm(IHttpRequest request)
		{
			if (!this.IsLogedIn(request))
			{
				return Redirect("/Users/Login");
			}

			string name = request.FormData["name"].ToString();
			string cover = request.FormData["cover"].ToString();

			using (var context = new RunesDbContext())
			{
				context.Albums.Add(new Album { Name = name, Cover = cover });
				context.SaveChanges();
			}

			return Redirect("/Albums/All");
		}

		public IHttpResponse Details(IHttpRequest request)
		{
			if (!this.IsLogedIn(request))
			{
				return Redirect("/Users/Login");
			}

			string id = request.QueryData["id"].ToString();

			using(var context = new RunesDbContext())
			{
				var album = context.Albums.FirstOrDefault(a => a.Id == id);
				ViewData["album"] = album.AlbumDetailsToHtml();
				ViewData["cover"] = album.Cover;
				ViewData["createTrackHref"] = $"/Tracks/Create?albumId={album.Id}";

				var tracks = context.Tracks
					.Where(t => t.AlbumId == album.Id)
					.ToList()
					.Select(t => t.TrackDetailsToHtml());
				ViewData["tracks"] = string.Join("", tracks);
			}

			return View();
		}
	}
}
