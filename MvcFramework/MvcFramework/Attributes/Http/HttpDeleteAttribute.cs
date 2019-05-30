using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes.Http
{
	public class HttpDeleteAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Delete;
	}
}
