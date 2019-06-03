using MvcFramework.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;
using System.Text;

namespace MvcFramework.Results
{
	public class JsonResult : ActionResult
	{
		public JsonResult(string jsonContent, HttpResponseStatusCode statusCode = HttpResponseStatusCode.Ok) : base(statusCode)
		{
			this.AddHeader(HttpHeader.ContentType, MimeTypes.ApplicationJson);
			this.Content = Encoding.UTF8.GetBytes(jsonContent);
		}
	}
}
