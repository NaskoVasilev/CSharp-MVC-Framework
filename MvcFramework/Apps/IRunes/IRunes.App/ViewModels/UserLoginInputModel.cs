using MvcFramework.Attributes.Validation;

namespace IRunes.App.ViewModels
{
	public class UserLoginInputModel
	{
		private const string ErrorMessage = "Invalid username or password!";

		[Required(ErrorMessage)]
		public string Username { get; set; }
		
		[Required(ErrorMessage)]
		public string Password { get; set; }
	}
}
