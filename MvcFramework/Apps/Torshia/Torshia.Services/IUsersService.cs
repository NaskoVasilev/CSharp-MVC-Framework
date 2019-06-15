using System.Collections.Generic;
using Torshia.Data.Models;

namespace Torshia.Services
{
	public interface IUsersService
	{
		bool UserExists(string username);

		string CreateUser(User user);

		User GetUserOrNull(string username, string password);

		string GetUserIdByUsername(string username);

		ICollection<string> GetUserRoles(string userId);

		bool AddToRole(string roleName, string userId);

		IEnumerable<string> GetIdsByNames(IEnumerable<string> usernames);
	}
}
