using IRunes.App.Extensions;
using IRunes.Models;
using IRunes.Services;
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
		private readonly IAlbumService albumService;

		public AlbumsController()
		{
			albumService = new AlbumService();
		}

		public IActionResult All()
		{

			var albums = albumService.GetAllAlbums()
				.Select(a => a.ToHtml())
				.ToList();

			if (albums.Count == 0)
			{
				ViewData["albums"] = "There are currently no albums.";
			}
			else
			{
				ViewData["albums"] = string.Join("", albums);
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

			Album album = new Album { Name = name, Cover = cover };
			albumService.CreateAlbum(album);

			return Redirect("/Albums/All");
		}

		public IActionResult Details()
		{
			string id = Request.QueryData["id"].ToString();


			var album = albumService.GetAlbumById(id);
			ViewData["album"] = album.AlbumDetailsToHtml();
			ViewData["cover"] = album.Cover;
			ViewData["createTrackHref"] = $"/Tracks/Create?albumId={album.Id}";

			var tracks = album.Tracks
				.ToList()
				.Select(t => t.TrackDetailsToHtml());
			ViewData["tracks"] = string.Join("", tracks);

			return View();
		}
	}
}
