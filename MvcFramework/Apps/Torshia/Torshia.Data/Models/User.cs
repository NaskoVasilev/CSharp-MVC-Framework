using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Torshia.Data.Models
{
	public class User : BaseModel
	{
		public User()
		{
			this.Roles = new HashSet<UserRole>();
		}

		[Required]
		[MaxLength(50)]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[MaxLength(50)]
		public string Email { get; set; }

		public ICollection<UserRole> Roles { get; set; }
	}
}
