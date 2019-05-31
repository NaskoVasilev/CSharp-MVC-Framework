using System.Collections.Generic;

namespace MvcFramework.Identity
{
	public class Principal
	{
		public Principal()
		{
			this.Roles = new HashSet<string>();
		}

		public Principal(string id, string username, string email) : this()
		{
			Id = id;
			Username = username;
			Email = email;
		}

		public string Id { get; set; }

		public string Username { get; set; }

		public string Email { get; set; }

		public HashSet<string> Roles { get; set; }
	}

}
