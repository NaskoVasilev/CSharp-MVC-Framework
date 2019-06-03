using System.ComponentModel.DataAnnotations;

namespace IRunes.Models
{
	public class Track : BaseModel
	{
		public string Name { get; set; }

		public string Link { get; set; }

		public decimal Price { get; set; }

		public string AlbumId { get; set; }
		public Album Album { get; set; }
	}
}