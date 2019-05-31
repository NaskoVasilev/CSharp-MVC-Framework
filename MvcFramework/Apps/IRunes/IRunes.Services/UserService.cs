using IRunes.Data;
using IRunes.Models;
using System.Linq;

namespace IRunes.Services
{
	public class UserService : IUserService
	{
		private readonly RunesDbContext context;

		public UserService()
		{
			this.context = new RunesDbContext();
		}

		public User CreateUser(User user)
		{
			context.Users.Add(user);
			context.SaveChanges();
			return user;
		}

		public User GetUserByUsernameAndPassword(string username, string password)
		{
			User user = context.Users.FirstOrDefault(u => (u.Username == username ||
			u.Email == username) && u.Password == password);
			return user;
		}

		public User GetUserByUsernameOrEmail(string username, string email)
		{
			return context.Users.FirstOrDefault(u => u.Username == username || u.Email == email);
		}
	}
}
