using MvcFramework.Attributes.Validation;

namespace IRunes.App.ViewModels
{
	public class AlbumCreateInputModel
	{
		[StringLength(3, 30, "Error Message")]
		public string Name { get; set; }

		[StringLength(3, 300, "Cover Error Message")]
		public string Cover { get; set; }
	}
}
