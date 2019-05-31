using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using System.Linq;

namespace IRunes.App.Controllers
{
	[Authorize]
	public class AlbumsController : Controller
	{
		public IActionResult All()
		{
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

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost(ActionName = nameof(Create))]
		public IActionResult CreateConfirm()
		{
			string name = Request.FormData["name"].ToString();
			string cover = Request.FormData["cover"].ToString();

			using (var context = new RunesDbContext())
			{
				context.Albums.Add(new Album { Name = name, Cover = cover });
				context.SaveChanges();
			}

			return Redirect("/Albums/All");
		}

		public IActionResult Details()
		{
			string id = Request.QueryData["id"].ToString();

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
