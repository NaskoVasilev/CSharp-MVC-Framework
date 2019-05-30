using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Responses;

namespace MvcFramework.Results
{
	public abstract class ActionResult : HttpResponse, IActionResult
	{
		public ActionResult(HttpResponseStatusCode statusCode) : base(statusCode)
		{
		}
	}
}
