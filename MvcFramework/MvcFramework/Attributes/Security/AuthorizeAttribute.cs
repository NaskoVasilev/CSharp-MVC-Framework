using MvcFramework.Identity;
using System;
using System.Linq;

namespace MvcFramework.Attributes.Security
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class AuthorizeAttribute : Attribute
	{
		public AuthorizeAttribute()
		{
		}

		public AuthorizeAttribute(string roles)
		{
			Roles = roles;
		}

		public string Roles { get; }

		private bool IsLoggedIn(Principal user)
		{
			return user != null;
		}

		public bool IsAuthorized(Principal user)
		{
			if (user == null)
			{
				return false;
			}

			if (string.IsNullOrEmpty(Roles))
			{
				return this.IsLoggedIn(user);
			}

			string[] roles = Roles.Split(", ", StringSplitOptions.RemoveEmptyEntries);
			return roles.All(role => user.Roles.Contains(role));
		}
	}
}
