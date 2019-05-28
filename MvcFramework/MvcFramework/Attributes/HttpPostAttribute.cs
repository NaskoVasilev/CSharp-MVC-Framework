using System;
using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes
{
	public class HttpPostAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Post;
	}
}
