using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;

namespace MvcFramework.Results
{
	public class InlineResourceResult : ActionResult
	{
		public InlineResourceResult(byte[] content, HttpResponseStatusCode statusCode) : base(statusCode)
		{
			this.AddHeader(HttpHeader.ContentLength, content.Length.ToString());
			this.AddHeader(HttpHeader.ContentDisposition, "inline");
			this.Content = content;
		}
	}
}
