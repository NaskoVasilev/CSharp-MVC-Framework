﻿using SULS.Data;
using SULS.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SULS.Services
{
	public class UsersService : IUsersService
	{
		private readonly SULSContext context;

		public UsersService(SULSContext context)
		{
			this.context = context;
		}

		public string CreateUser(User user)
		{
			user.Password = HashPassword(user.Password);
			context.Users.Add(user);
			context.SaveChanges();
			return user.Id;
		}

		public string GetUserIdByUsername(string username)
		{
			string userId = this.context.Users.Where(u => u.Username == username)
				.Select(u => u.Id)
				.FirstOrDefault();
			return userId;
		}

		public User GetUserOrNull(string username, string password)
		{
			string hashedPassword = HashPassword(password);
			User user = this.context.Users.SingleOrDefault(u => u.Username == username && u.Password == hashedPassword);
			return user;
		}

		public bool UserExists(string username)
		{
			return this.context.Users.Any(u => u.Username == username);
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
