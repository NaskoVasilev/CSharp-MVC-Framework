using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using MvcFramework;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;

namespace IRunes.App.Controllers
{
	[Authorize]
	public class TracksController : Controller
	{
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

		public IActionResult Details()
		{
			string id = Request.QueryData["id"].ToString();

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
