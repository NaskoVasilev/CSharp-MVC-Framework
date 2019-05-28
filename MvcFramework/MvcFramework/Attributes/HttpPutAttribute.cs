using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes
{
	public class HttpPutAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Put;
	}
}
