using System;
using System.Collections;
using System.Collections.Generic;

namespace MvcFramework.AutoMapper
{
	internal static class ReflectionUtils
	{
		private static readonly HashSet<string> types = new HashSet<string>()
		{
			"System.String", "System.Int32",  "System.Decimal",
			"System.Double", "System.Guid", "System.Single",
			"System.Int64", "System.UInt64", "System.Int16",
			"System.DateTime", "System.String[]", "System.Int32[]",
			"System.Decimal[]", "System.Double[]", "System.Guid[]",
			"System.Single[]", "System.DateTime[]"
		};

		internal static bool IsPrimitive(Type type)
		{
			return types.Contains(type.FullName)
				|| type.IsPrimitive
				|| (IsNullable(type) && IsPrimitive(Nullable.GetUnderlyingType(type)))
				|| type.IsEnum;
		}

		internal static bool IsGenericCollection(Type type)
		{
			return
				(type.IsGenericType &&
				(type.GetGenericTypeDefinition() == typeof(List<>)
				|| type.GetGenericTypeDefinition() == typeof(ICollection<>)
				|| type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
				|| type.GetGenericTypeDefinition() == typeof(IList<>)))
				|| typeof(IList<>).IsAssignableFrom(type)
				|| typeof(HashSet<>).IsAssignableFrom(type);
		}

		internal static bool IsNonGenericCollection(Type type)
		{
			return type.IsArray
				|| type == typeof(ArrayList)
				|| typeof(IList).IsAssignableFrom(type);
		}

		internal static bool IsCollection(Type type)
		{
			return typeof(IEnumerable).IsAssignableFrom(type);
		}

		private static bool IsNullable(Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}
	}
}
