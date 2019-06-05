using System.Collections.Generic;

namespace MvcFramework.HTTP.EqualityComaperers
{
	public class StringCaseInsensitiveEqualityComaparer : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return x.ToLower() == y.ToLower();
		}

		public int GetHashCode(string obj)
		{
			return obj.ToLower().GetHashCode();
		}
	}
}
