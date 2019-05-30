using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;
using System.Text;

namespace MvcFramework.Results
{
	public class XmlResult : ActionResult
	{
		public XmlResult(string xmlContent, HttpResponseStatusCode statusCode = HttpResponseStatusCode.Ok) : base(statusCode)
		{
			this.AddHeader(HttpHeader.ContentType, MimeTypes.ApplicationXml);
			this.Content = Encoding.UTF8.GetBytes(xmlContent);
		}
	}
}
