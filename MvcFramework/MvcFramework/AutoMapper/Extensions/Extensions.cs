using System.Collections.Generic;
using System.Linq;

namespace MvcFramework.AutoMapper.Extensions
{
	public static class Extensions
	{
		public static IEnumerable<TDest> MapTo<TDest>(this IEnumerable<object> sourceCollection)
		{
			return sourceCollection.Select(Mapper.MapTo<TDest>);
		}

		public static TDest MapTo<TDest>(this object source)
		{
			return Mapper.MapTo<TDest>(source);
		}
	}
}
