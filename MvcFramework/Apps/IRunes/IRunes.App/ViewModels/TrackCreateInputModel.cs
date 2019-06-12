using MvcFramework.Attributes.Validation;

namespace IRunes.App.ViewModels
{
	public class TrackCreateInputModel
	{
		public string AlbumId { get; set; }

		[StringLength(3, 50)]
		public string Name { get; set; }

		[Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", "The link must be hyper link for embeding in iframe")]
		public string Link { get; set; }

		[Range(typeof(decimal), "0.01", "1000.00")]
		public decimal Price { get; set; }
	}
}
