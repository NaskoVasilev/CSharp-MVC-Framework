using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
	public class User : BaseModel
	{
		public User()
		{
			this.Submissions = new HashSet<Submission>();
		}

		[Required]
		[MaxLength]
		public string Username { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public ICollection<Submission>	Submissions { get; set; }
	}
}