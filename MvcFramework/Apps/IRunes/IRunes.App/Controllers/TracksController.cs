using IRunes.App.Extensions;
using IRunes.Models;
using IRunes.Services;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;

namespace IRunes.App.Controllers
{
	[Authorize]
	public class TracksController : Controller
	{
		private readonly ITrackService trackService;
		private readonly IAlbumService albumService;

		public TracksController()
		{
			this.trackService = new TrackService();
			this.albumService = new AlbumService();
		}

		public IActionResult Create()
		{
			ViewData["albumId"] = Request.QueryData["albumId"].ToString();
			return View();
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

			Track track = trackService.GetById(id);
			ViewData["track"] = track.ToTrackDetialsHtml();
			ViewData["link"] = track.Link;
			ViewData["albumId"] = track.AlbumId;

			return View();
		}
	}
}
