using IRunes.App.ViewModels;
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
				.Select(a => new AlbumInfoViewModel { Id= a.Id, Name = a.Name })
				.ToList();

			return View(new AlbumsAllViewModel { Albums = albums });
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
			var model = new AlbumDetailsViewModel
			{
				Name = album.Name,
				Price = album.Price,
				Cover = album.Cover,
				Id = album.Id,
				Tracks = album.Tracks
				.Select(t => new TrackAlbumDetailsViewModel { Id = t.Id, Name = t.Name })
				.ToList()
			};
		
			return View(model);
		}
	}
}
