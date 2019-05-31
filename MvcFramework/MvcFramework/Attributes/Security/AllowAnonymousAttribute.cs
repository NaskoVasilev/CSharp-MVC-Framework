using System;

namespace MvcFramework.Attributes.Security
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AllowAnonymousAttribute :  Attribute
	{
	}
}
