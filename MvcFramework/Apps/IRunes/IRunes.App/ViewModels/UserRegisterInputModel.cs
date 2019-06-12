using MvcFramework.Attributes.Validation;

namespace IRunes.App.ViewModels
{
	public class UserRegisterInputModel
	{
		[StringLength(3, 30)]
		public string Username { get; set; }

		[StringLength(3, 30)]
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		[Email]
		public string Email { get; set; }
	}
}
