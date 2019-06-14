using Musaca.Data.Models;
using System.Collections.Generic;

namespace Musaca.Services
{
	public interface IUsersService
	{
		bool UserExists(string username);

		string CreateUser(User user);

		User GetUserOrNull(string username, string password);

		IEnumerable<string> GetUsernames();

		string GetUserIdByUsername(string username);
	}
}
