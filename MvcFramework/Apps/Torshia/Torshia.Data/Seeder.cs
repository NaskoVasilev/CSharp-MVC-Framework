using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Torshia.Data.Models;

namespace Torshia.Data
{
	public static class Seeder
	{
		public static void SeedRoles()
		{
			using (TorshiaDbContext context = new TorshiaDbContext())
			{
				AddRoleIfNotExists("User");
				AddRoleIfNotExists("Admin");
			}
		}

		public static void SeedAdmin()
		{
			using (TorshiaDbContext context = new TorshiaDbContext())
			{
				string userRoleId = GetRoleId("User");
				string adminRoleId = GetRoleId("Admin");

				var userRoles = new HashSet<UserRole>()
				{
					new UserRole{ RoleId = userRoleId },
					new UserRole{ RoleId = adminRoleId }
				};

				context.Users.Add(new User
				{
					Email = "Admin@mail.com",
					Password = HashPassword("admin"),
					Username = "admin",
					Roles = userRoles
				});

				context.SaveChanges();
			}
		}

		private static void AddRoleIfNotExists(string roleName)
		{
			using (TorshiaDbContext context = new TorshiaDbContext())
			{
				if (!context.Roles.Any(r => r.Name == roleName))
				{
					context.Roles.Add(new Role { Name = roleName });
				}

				context.SaveChanges();
			}
		}

		private static string GetRoleId(string roleName)
		{
			using (var contxt = new TorshiaDbContext())
			{
				return contxt.Roles.FirstOrDefault(r => r.Name == roleName)?.Id;
			}
		}

		private static string HashPassword(string password)
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
