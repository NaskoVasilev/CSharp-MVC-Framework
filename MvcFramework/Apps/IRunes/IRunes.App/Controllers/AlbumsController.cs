using IRunes.App.ViewModels;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using System.Linq;
using MvcFramework.AutoMapper.Extensions;

namespace IRunes.App.Controllers
{
	[Authorize]
	public class AlbumsController : Controller
	{
		private readonly IAlbumService albumService;

		public AlbumsController(IAlbumService albumService)
		{
			this.albumService = albumService;
		}

		public IActionResult All()
		{
			var albums = albumService.GetAllAlbums()
				.MapTo<AlbumInfoViewModel>()
				.ToList();

			return View(albums);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost(ActionName = nameof(Create))]
		public IActionResult CreateConfirm()
		{
			string name = Request.FormData["name"].FirstOrDefault();
			string cover = Request.FormData["cover"].FirstOrDefault();

			Album album = new Album { Name = name, Cover = cover };
			albumService.CreateAlbum(album);

			return Redirect("/Albums/All");
		}

		public IActionResult Details()
		{
			string id = Request.QueryData["id"].FirstOrDefault();
			AlbumDetailsViewModel album = albumService.GetAlbumById(id).MapTo<AlbumDetailsViewModel>();
		
			return View(album);
		}
	}
}
