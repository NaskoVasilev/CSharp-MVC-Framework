using System;
using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes
{
	public class HttpGetAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Get;
	}
}
