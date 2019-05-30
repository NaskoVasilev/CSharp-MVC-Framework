using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;

namespace MvcFramework.Results
{
	public class JsonResult : ActionResult
	{
		public JsonResult(HttpResponseStatusCode statusCode) : base(statusCode)
		{
			this.AddHeader(GlobalConstants.ContentTypeHeaderKey, GlobalConstants.ContentTypeHeaderJsonValue);
		}
	}
}
