using MvcFramework.Attributes.Validation;

namespace Panda.Web.ViewModels.Users
{
	public class RegisterInputModel
	{
		[Required]
		[StringLength(5, 20)]
		public string Username { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		[Required]
		[StringLength(5, 20)]
		public string Email { get; set; }
	}
}
