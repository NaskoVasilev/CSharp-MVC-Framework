using IRunes.Models;

namespace IRunes.Services
{
	public interface IUserService
	{
		User GetUserByUsernameAndPassword(string username, string password);

		User GetUserByUsernameOrEmail(string username, string email);

		User CreateUser(User user);
	}
}
