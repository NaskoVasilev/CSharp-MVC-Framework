using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;
using MvcFramework.HTTP.Responses;
using System.Text;

namespace MvcFramework.Results
{
	public class TextResult : HttpResponse
	{
		public TextResult(string content, HttpResponseStatusCode statusCode, 
			string contentType = GlobalConstants.ContentTypeHeaderTextValue) : base(statusCode)
		{
			this.Headers.AddHeader(new HttpHeader(GlobalConstants.ContentTypeHeaderKey, contentType));
			this.Content = Encoding.UTF8.GetBytes(content);
		}

		public TextResult(byte[] content, HttpResponseStatusCode statusCode, 
			string contentType = GlobalConstants.ContentTypeHeaderTextValue) : base(statusCode)
		{
			this.Headers.AddHeader(new HttpHeader(GlobalConstants.ContentTypeHeaderKey, contentType));
			this.Content = content;	
		}
	}
}
