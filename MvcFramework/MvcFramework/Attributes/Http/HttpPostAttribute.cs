using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
	public class HttpPostAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Post;
	}
}
