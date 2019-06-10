using MvcFramework.Attributes.Validation;

namespace IRunes.App.ViewModels
{
	public class UserLoginInputModel
	{
		[Required]
		[StringLength(3, 30, nameof(Username))]
		public string Username { get; set; }

		[StringLength(3, 30, nameof(Password))]
		public string Password { get; set; }
	}
}
