using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;

namespace MvcFramework.Results
{
	public class RedirectResult : ActionResult
	{
		public RedirectResult(string location) : base(HttpResponseStatusCode.SeeOther)
		{
			this.AddHeader(HttpHeader.Location, location);
		}
	}
}
