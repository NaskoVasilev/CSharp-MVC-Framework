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

		public AlbumsController()
		{
			albumService = new AlbumService();
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
			string name = Request.FormData["name"].ToString();
			string cover = Request.FormData["cover"].ToString();

			Album album = new Album { Name = name, Cover = cover };
			albumService.CreateAlbum(album);

			return Redirect("/Albums/All");
		}

		public IActionResult Details()
		{
			string id = Request.QueryData["id"].ToString();
			AlbumDetailsViewModel album = albumService.GetAlbumById(id).MapTo<AlbumDetailsViewModel>();
		
			return View(album);
		}
	}
}
