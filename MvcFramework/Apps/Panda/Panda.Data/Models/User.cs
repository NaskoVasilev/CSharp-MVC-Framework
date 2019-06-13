using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Panda.Data.Models
{
	public class User : BaseModel
	{
		[Required]
		[MaxLength(20)]
		public string Username { get; set; }

		[Required]
		[MinLength(20)]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public ICollection<Package> Packages { get; set; }

		public ICollection<Receipt> Receipts { get; set; }
	}
}
