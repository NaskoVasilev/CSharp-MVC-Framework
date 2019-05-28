using MvcFramework.HTTP.Enums;

namespace MvcFramework.Attributes
{
	public class HttpDeleteAttribute : BaseHttpAttribute
	{
		public override HttpRequestMethod HttpRequestMethod => HttpRequestMethod.Delete;
	}
}
