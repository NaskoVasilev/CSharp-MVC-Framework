using MvcFramework.Attributes.Validation;

namespace IRunes.App.ViewModels
{
	public class AlbumCreateInputModel
	{
		[StringLength(3, 30, nameof(Name))]
		public string Name { get; set; }

		[Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$")]
		[StringLength(3, 300, "Cover must be hyperlink with length between 3 and 300")]
		public string Cover { get; set; }
	}
}
