using Panda.Data.Models;
using System.Collections.Generic;

namespace Panda.Services
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
