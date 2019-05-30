using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
	public class HttpGetAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Get;
	}
}
