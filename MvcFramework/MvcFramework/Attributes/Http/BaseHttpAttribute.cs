using MvcFramework.HTTP.Enums;
using System;

namespace MvcFramework.Attributes.Http
{
	[AttributeUsage(AttributeTargets.Method)]
	public abstract class BaseHttpAttribute : Attribute
	{
		public string ActionName { get; set; }

		public string Url { get; set; }

		public abstract HttpRequestMethod HttpRequestMethod { get; }

		public BaseHttpAttribute()
		{
		}

		public BaseHttpAttribute(string url)
		{
			Url = url;
		}

		public BaseHttpAttribute(string url, string actionName) : this(url)
		{
			ActionName = actionName;
		}
	}
}
