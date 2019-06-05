using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcFramework.DependencyContainer
{
	public class ServiceProvider : IServiceProvider
	{ 
		private readonly IDictionary<Type, Type> dependencyContainer;

		public ServiceProvider()
		{
			this.dependencyContainer = new ConcurrentDictionary<Type, Type>();
		}

		public void Add<TInterface, TImplementation>() where TImplementation : TInterface
		{
			dependencyContainer[typeof(TInterface)] = typeof(TImplementation);
		}

		public object CreateInstance(Type type)
		{
			if(dependencyContainer.ContainsKey(type))
			{
				type = dependencyContainer[type];
			}

			ConstructorInfo constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
				.OrderBy(c => c.GetParameters().Count())
				.FirstOrDefault();

			if(constructor == null)
			{
				return null;
			}

			List<object> dependencies = new List<object>();

			foreach (var parameter in constructor.GetParameters())
			{
				dependencies.Add(CreateInstance(parameter.ParameterType));
			}

			return constructor.Invoke(dependencies.ToArray());
		}

		public T CteateInstance<T>()
		{
			return (T) CreateInstance(typeof(T));
		}
	}
}
