using System.ComponentModel.DataAnnotations;

namespace IRunes.Models
{
	public class User : BaseModel
	{
		[Required]
		[MaxLength(20)]
		public string Username { get; set; }

		public string Password { get; set; }

		[Required]
		[MaxLength(20)]
		public string Email { get; set; }
	}
}
