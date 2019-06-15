using System.ComponentModel.DataAnnotations;

namespace Torshia.Data.Models
{
	public class UserRole
	{
		[Required]
		public string RoleId { get; set; }
		public Role Role { get; set; }

		[Required]
		public string UserId  { get; set; }
		public User User { get; set; }
	}
}
