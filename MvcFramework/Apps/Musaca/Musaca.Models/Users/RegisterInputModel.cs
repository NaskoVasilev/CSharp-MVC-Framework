using MvcFramework.Attributes.Validation;

namespace Musaca.Models.Users
{
	public class RegisterInputModel
	{
		[Required]
		[StringLength(5, 20)]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		[Email]
		[StringLength(5, 20)]
		public string Email { get; set; }
	}
}
