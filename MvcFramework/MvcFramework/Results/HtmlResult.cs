using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;
using System.Text;

namespace MvcFramework.Results
{
	public class HtmlResult : ActionResult
	{
		public HtmlResult(string content, HttpResponseStatusCode statusCode) : base(statusCode)
		{
			this.Headers.AddHeader(new HttpHeader(GlobalConstants.ContentTypeHeaderKey, GlobalConstants.ContentTypeHeaderHtmlValue));
			this.Content = Encoding.UTF8.GetBytes(content);
		}
	}
}
