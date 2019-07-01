using SULS.Models;

namespace SULS.Services
{
	public interface IUsersService
	{
		bool UserExists(string username);

		string CreateUser(User user);

		User GetUserOrNull(string username, string password);

		string GetUserIdByUsername(string username);
	}
}
