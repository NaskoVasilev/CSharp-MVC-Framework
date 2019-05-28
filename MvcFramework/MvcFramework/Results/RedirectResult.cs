using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;
using MvcFramework.HTTP.Responses;

namespace MvcFramework.Results
{
	public class RedirectResult : HttpResponse
	{
		public RedirectResult(string location) : base(HttpResponseStatusCode.SeeOther)
		{
			this.AddHeader(new HttpHeader(GlobalConstants.LocationHeaderKey, location));
		}
	}
}
