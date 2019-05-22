using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Cookies;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;
using MvcFramework.HTTP.Responses;
using System.Text;

namespace MvcFramework.WebServer.Results
{
	public class HtmlResult : HttpResponse
	{
		public HtmlResult(string content, HttpResponseStatusCode statusCode) : base(statusCode)
		{
			this.Headers.AddHeader(new HttpHeader(GlobalConstants.ContentTypeHeaderKey, GlobalConstants.ContentTypeHeaderHtmlValue));
			this.Content = Encoding.UTF8.GetBytes(content);
		}
	}
}
