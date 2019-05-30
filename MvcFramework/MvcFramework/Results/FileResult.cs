using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;

namespace MvcFramework.Results
{
	public class FileResult : ActionResult
	{
		public FileResult(byte[] fileContent, HttpResponseStatusCode statusCode = HttpResponseStatusCode.Ok) 
			: base(statusCode)
		{
			this.AddHeader(HttpHeader.ContentLength, fileContent.Length.ToString());
			this.AddHeader(HttpHeader.ContentDisposition, "attachment");
			this.Content = fileContent;
		}
	}
}
