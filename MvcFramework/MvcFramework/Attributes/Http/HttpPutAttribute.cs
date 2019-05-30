using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
	public class HttpPutAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Put;
	}
}
