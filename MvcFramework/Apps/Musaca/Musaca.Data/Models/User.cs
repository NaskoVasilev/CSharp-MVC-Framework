using System.ComponentModel.DataAnnotations;

namespace Musaca.Data.Models
{
	public class User : BaseModel
	{
		[Required]
		[MaxLength(20)]
		public string Username { get; set; }

		[Required]
		[MaxLength(50)]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
