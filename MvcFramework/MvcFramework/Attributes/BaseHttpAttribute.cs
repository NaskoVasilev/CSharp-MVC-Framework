using MvcFramework.HTTP.Enums;
using System;

namespace MvcFramework.Attributes
{
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
			this.Url = url;
		}

		public BaseHttpAttribute(string url, string actionName) : this(url)
		{
			this.ActionName = actionName;
		}
	}
}
