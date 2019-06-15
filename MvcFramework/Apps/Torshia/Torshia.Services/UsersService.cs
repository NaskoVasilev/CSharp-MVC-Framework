using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Torshia.Data;
using Torshia.Data.Models;

namespace Torshia.Services
{
	public class UsersService : IUsersService
	{
		private readonly TorshiaDbContext context;

		public UsersService(TorshiaDbContext context)
		{
			this.context = context;
		}

		public bool AddToRole(string roleName, string userId)
		{
			Role role = context.Roles.FirstOrDefault(r => r.Name == roleName);
			if(role == null)
			{
				return false;
			}

			UserRole userRole = new UserRole() { RoleId = role.Id, UserId = userId };
			context.UserRoles.Add(userRole);
			context.SaveChanges();
			return true;
		}

		public string CreateUser(User user)
		{
			user.Password = HashPassword(user.Password);
			context.Users.Add(user);
			context.SaveChanges();
			return user.Id;
		}

		public IEnumerable<string> GetIdsByNames(IEnumerable<string> usernames)
		{
			var ids = context.Users.Where(u => usernames.Contains(u.Username))
				.Select(u => u.Id)
				.ToList();

			return ids;
		}

		public string GetUserIdByUsername(string username)
		{
			string userId = context.Users.Where(u => u.Username == username)
				.Select(u => u.Id)
				.FirstOrDefault();
			return userId;
		}

		public User GetUserOrNull(string username, string password)
		{
			string hashedPassword = HashPassword(password);
			User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == hashedPassword);
			return user;
		}

		public ICollection<string> GetUserRoles(string userId)
		{
			var roles = context.UserRoles.Where(ur => ur.UserId == userId)
				.Select(ur => ur.Role.Name)
				.ToList();

			return roles;
		}

		public bool UserExists(string username)
		{
			return context.Users.Any(u => u.Username == username);
		}

		private string HashPassword(string password)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
