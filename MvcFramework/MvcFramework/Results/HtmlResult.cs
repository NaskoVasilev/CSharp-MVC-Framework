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
			this.AddHeader(HttpHeader.ContentType, MimeTypes.TextHtml);
			this.Content = Encoding.UTF8.GetBytes(content);
		}
	}
}
