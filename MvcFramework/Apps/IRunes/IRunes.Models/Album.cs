namespace IRunes.Models
{
	using System.Collections.Generic;

	public class Album : BaseModel
	{
		public Album()
		{
			this.Tracks = new List<Track>();
		}

		public string Name { get; set; }

		public string Cover { get; set; }

		public decimal Price { get; set; }

		public ICollection<Track> Tracks { get; set; }
	}
}
