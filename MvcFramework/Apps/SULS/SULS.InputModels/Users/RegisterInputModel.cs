using MvcFramework.Attributes.Validation;

namespace SULS.InputModels.Users
{
	public class RegisterInputModel
	{
		[Required]
		[StringLength(5, 20)]
		public string Username { get; set; }

		[Required]
		[StringLength(6, 20)]
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		[Required]
		[Email]
		public string Email { get; set; }
	}
}
