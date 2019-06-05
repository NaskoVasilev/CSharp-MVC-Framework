using System;

namespace MvcFramework.DependencyContainer
{
	public interface IServiceProvider
	{
		void Add<TInterface, TImplementation>() where TImplementation : TInterface;

		object CreateInstance(Type type);

		T CteateInstance<T>();
	}
}
