using MvcFramework.Attributes.Validation;

namespace Torshia.Models.Users
{
	public class RegisterInputModel
	{
		[Required]
		[StringLength(5, 50)]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		[Email]
		[StringLength(5, 50)]
		public string Email { get; set; }
	}
}
