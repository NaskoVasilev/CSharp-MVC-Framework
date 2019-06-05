using IRunes.App.ViewModels;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.AutoMapper.Extensions;
using MvcFramework.Results;

namespace IRunes.App.Controllers
{
	[Authorize]
	public class TracksController : Controller
	{
		private readonly ITrackService trackService;
		private readonly IAlbumService albumService;

		public TracksController(ITrackService trackService, IAlbumService albumService)
		{
			this.trackService = trackService;
			this.albumService = albumService;
		}

		public IActionResult Create()
		{
			string albumId = Request.QueryData["albumId"].ToString();
			return View(new TrackCreateViewModel { AlbumId = albumId });
		}

		[HttpPost(ActionName = nameof(Create))]
		public IActionResult CreateConfirm()
		{
			string albumId = Request.QueryData["albumId"].ToString();
			string name = Request.FormData["name"].ToString();
			string link = Request.FormData["link"].ToString();
			decimal price = decimal.Parse(Request.FormData["price"].ToString());

			Album album = albumService.GetById(albumId);
			if (album == null)
			{
				return Redirect("/Tracks/Create?albumId=" + albumId);
			}

			Track track = new Track { Name = name, Link = link, Price = price, AlbumId = album.Id };
			album.Price += price * GlobalConstants.PriceDiscountConeficient;
			trackService.CreateTrack(track);
			albumService.UpdateAlbum(album);

			return Redirect("/Albums/Details?id=" + albumId);
		}

		public IActionResult Details()
		{
			string id = Request.QueryData["id"].ToString();
			TrackDetailsViewModel track = trackService.GetById(id).MapTo<TrackDetailsViewModel>();
			
			return View(track);
		}
	}
}
