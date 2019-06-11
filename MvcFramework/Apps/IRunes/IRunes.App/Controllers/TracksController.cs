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

		public IActionResult Create(string albumId)
		{
			return View(new TrackCreateViewModel { AlbumId = albumId });
		}

		[HttpPost]
		public IActionResult Create(TrackCreateInputModel model)
		{
			if(!ModelState.IsValid)
			{
				return Redirect("/Tracks/Create?albumId=" + model.AlbumId);
			}

			Album album = albumService.GetById(model.AlbumId);
			if (album == null)
			{
				return Redirect("/Tracks/Create?albumId=" + model.AlbumId);
			}

			Track track = model.MapTo<Track>();
			album.Price += model.Price * GlobalConstants.PriceDiscountConeficient;
			trackService.CreateTrack(track);
			albumService.UpdateAlbum(album);

			return Redirect("/Albums/Details?id=" + model.AlbumId);
		}

		public IActionResult Details(string id)
		{ 
			TrackDetailsViewModel track = trackService.GetById(id).MapTo<TrackDetailsViewModel>();
			
			return View(track);
		}
	}
}
