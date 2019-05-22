using System;

namespace MvcFramework.Models
{
	public class User
	{
		public User()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		public string Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }
	}
}
