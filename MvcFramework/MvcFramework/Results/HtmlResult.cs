using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using System.Text;

namespace MvcFramework.Results
{
	public class HtmlResult : ActionResult
	{
		public HtmlResult(string content, HttpResponseStatusCode statusCode) : base(statusCode)
		{
			this.AddHeader(GlobalConstants.ContentTypeHeaderKey, GlobalConstants.ContentTypeHeaderHtmlValue);
			this.Content = Encoding.UTF8.GetBytes(content);
		}
	}
}
