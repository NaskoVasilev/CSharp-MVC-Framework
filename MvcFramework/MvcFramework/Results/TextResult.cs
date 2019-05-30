using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using System.Text;

namespace MvcFramework.Results
{
	public class TextResult : ActionResult
	{
		public TextResult(string content, HttpResponseStatusCode statusCode, 
			string contentType = GlobalConstants.ContentTypeHeaderTextValue) : base(statusCode)
		{
			this.AddHeader(GlobalConstants.ContentTypeHeaderKey, contentType);
			this.Content = Encoding.UTF8.GetBytes(content);
		}

		public TextResult(byte[] content, HttpResponseStatusCode statusCode, 
			string contentType = GlobalConstants.ContentTypeHeaderTextValue) : base(statusCode)
		{
			this.AddHeader(GlobalConstants.ContentTypeHeaderKey, contentType);
			this.Content = content;	
		}
	}
}
