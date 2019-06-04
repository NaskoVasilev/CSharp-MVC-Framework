using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcFramework.Common;

namespace MvcFramework.AutoMapper
{
	public static class Mapper
	{
		public static IEnumerable<TDest> MapTo<TDest>(IEnumerable<object> sourceCollection)
		{
			return sourceCollection.Select(MapTo<TDest>);
		}

		public static TDest MapTo<TDest>(object source)
		{
			if (source == null)
			{
				throw new ArgumentException(ExceptionUtils.NullSource);
			}

			var dest = Activator.CreateInstance(typeof(TDest));
			return (TDest)MapObject(source, dest);
		}

		private static object MapObject(object source, object dest)
		{
			foreach (var destProp in dest.GetType()
			   .GetProperties(BindingFlags.Public | BindingFlags.Instance)
			   .Where(p => p.CanWrite))
			{
				var sourceProp = source.GetType()
				   .GetProperties(BindingFlags.Public | BindingFlags.Instance)
				   .FirstOrDefault(x => x.Name == destProp.Name);

				if (sourceProp != null)
				{
					var sourceValue = sourceProp.GetMethod.Invoke(source, new object[0]);

					if (sourceValue == null)
					{
						throw new ArgumentException(ExceptionUtils.NullableSourceValueGetMethod);
					}

					if (ReflectionUtils.IsPrimitive(sourceValue.GetType()))
					{
						destProp.SetValue(dest, sourceValue);
						continue;
					}

					if (ReflectionUtils.IsGenericCollection(sourceValue.GetType()))
					{
						if (ReflectionUtils.IsPrimitive(sourceValue.GetType().GetGenericArguments()[0]))
						{
							var destinationCollection = sourceValue;
							destProp.SetMethod.Invoke(dest, new[] { destinationCollection });
						}
						else
						{
							var destColl = destProp.GetMethod.Invoke(dest, new object[0]);
							if (destColl == null)
							{
								destColl = Activator.CreateInstance(destProp.PropertyType);
							}
							var destType = destColl.GetType().GetGenericArguments()[0];

							foreach (var value in (IEnumerable)sourceValue)
							{
								((IList)destColl).Add(CreateMappedObject(value, destType));
							}

							destProp.SetMethod.Invoke(dest, new object[] { destColl });
						}
					}
					else if (ReflectionUtils.IsNonGenericCollection(sourceValue.GetType()))
					{
						int sourceCollectionLength = ((object[])sourceValue).Length;
						var destColl = (IList)Activator.CreateInstance(destProp.PropertyType,
							new object[] { sourceCollectionLength });
						object[] sourceCollection = (object[])sourceValue;

						for (int i = 0; i < sourceCollectionLength; i++)
						{
							if (ReflectionUtils.IsPrimitive(destProp.PropertyType.GetElementType()))
							{
								destColl[i] = sourceCollection[i];
							}
							else
							{
								destColl[i] = CreateMappedObject(sourceCollection[i], destProp.PropertyType.GetElementType());
							}
						}

						destProp.SetValue(dest, destColl);
					}
					else
					{
						var value = CreateMappedObject(sourceValue, destProp.PropertyType);
						destProp.SetValue(dest, value);
					}
				}

			}

			return dest;
		}

		private static object CreateMappedObject(object source, Type destType)
		{
			if (source == null || destType == null)
			{
				throw new ArgumentException(ExceptionUtils.SourceOrDestTypeIsNull);
			}

			var dest = Activator.CreateInstance(destType);
			return MapObject(source, dest);
		}
	}
}
