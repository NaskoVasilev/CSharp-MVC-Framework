using System.Collections.Generic;
using System.Linq;

namespace MvcFramework.Utils
{
	public static class CastUtils
	{
		public static object CastCollectionToList(IEnumerable<object> collection, System.Type elementType)
		{
			if (elementType == typeof(int))
			{
				return collection.Cast<int>().ToList();
			}
			else if (elementType == typeof(double))
			{
				return collection.Cast<double>().ToList();
			}
			else if (elementType == typeof(string))
			{
				return collection.Cast<string>().ToList();
			}
			else if (elementType == typeof(decimal))
			{
				return collection.Cast<decimal>().ToList();
			}
			else if (elementType == typeof(long))
			{
				return collection.Cast<long>().ToList();
			}
			else if (elementType == typeof(bool))
			{
				return collection.Cast<bool>().ToList();
			}

			return collection;
		}

		public static object CastCollectionToArray(IEnumerable<object> collection, System.Type elementType)
		{
			if (elementType == typeof(int))
			{
				return collection.Cast<int>().ToArray();
			}
			else if (elementType == typeof(double))
			{
				return collection.Cast<double>().ToArray();
			}
			else if (elementType == typeof(string))
			{
				return collection.Cast<string>().ToArray();
			}
			else if (elementType == typeof(decimal))
			{
				return collection.Cast<decimal>().ToArray();
			}
			else if (elementType == typeof(long))
			{
				return collection.Cast<long>().ToArray();
			}
			else if (elementType == typeof(bool))
			{
				return collection.Cast<bool>().ToArray();
			}

			return collection;
		}
	}
}
