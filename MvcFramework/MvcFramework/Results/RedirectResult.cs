using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;

namespace MvcFramework.Results
{
	public class RedirectResult : ActionResult
	{
		public RedirectResult(string location) : base(HttpResponseStatusCode.SeeOther)
		{
			this.AddHeader(GlobalConstants.LocationHeaderKey, location);
		}
	}
}
