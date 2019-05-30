using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;

namespace MvcFramework.Results
{
	public class InlineResourceResult : ActionResult
	{
		public InlineResourceResult(byte[] content, HttpResponseStatusCode statusCode) : base(statusCode)
		{
			this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentLength, content.Length.ToString()));
			this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentDisposition, "inline"));
			this.Content = content;
		}
	}
}
