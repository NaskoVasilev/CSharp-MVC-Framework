namespace IRunes.App.Extensions
{
	using IRunes.Models;
	using System.Text;

	public static class EntityExtensions
	{
		public static string ToHtml(this Album album)
		{
			return $"<a class=\"d-block h3\" href=\"/Albums/Details?id={album.Id}\">{album.Name}</a>";
		}

		public static string AlbumDetailsToHtml(this Album album)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"<h5 class=\"text-center\">Name: {album.Name}</h5>");
			sb.Append($"<h5 class=\"text-center\">Price: ${album.Price:F2}</h5>");
			return sb.ToString().TrimEnd();
		}

		public static string TrackDetailsToHtml(this Track track)
		{
			return $"<li><a href=\"/Tracks/Details?id={track.Id}\"><i>{track.Name}</i></a></li>";
		}

		public static string ToTrackDetialsHtml(this Track track)
		{
			return $"<h3 class=\"text-center\">Name: {track.Name}</h3><h3 class=\"text-center\">Price: ${track.Price}</h3>";
		}
	}
}
