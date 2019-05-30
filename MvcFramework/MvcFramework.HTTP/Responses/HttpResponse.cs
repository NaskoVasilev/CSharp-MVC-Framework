using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Cookies;
using MvcFramework.HTTP.Cookies.Contracts;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Extensions;
using MvcFramework.HTTP.Headers;
using MvcFramework.HTTP.Headers.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using System.Text;

namespace MvcFramework.HTTP.Responses
{
	public class HttpResponse : IHttpResponse
	{
		public HttpResponse()
		{
			this.Headers = new HttpHeaderCollection();
			this.Cookies = new HttpCookieCollection();
			this.Content = new byte[0];
		}

		public HttpResponse(HttpResponseStatusCode statusCode) : this()
		{
			CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
			this.StatusCode = statusCode;
		}

		public HttpResponseStatusCode StatusCode { get; set; }

		public IHttpHeaderCollection Headers { get; }
			
		public IHttpCookieCollection Cookies { get; set; }

		public byte[] Content { get; set; }

		public void AddHeader(string key, string value)
		{
			CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
			CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
			this.Headers.AddHeader(new HttpHeader(key, value));
		}

		public void AddCookie(HttpCookie cookie)
		{
			CoreValidator.ThrowIfNull(cookie, nameof(cookie));

			this.Cookies.AddCookie(cookie);
		}

		public byte[] GetBytes()
		{
			byte[] responseWithoutBody = Encoding.UTF8.GetBytes(this.ToString());
			byte[] response = new byte[responseWithoutBody.Length + this.Content.Length];

			for (int i = 0; i < responseWithoutBody.Length; i++)
			{
				response[i] = responseWithoutBody[i];
			}

			int contentBufferSatrtIndex = responseWithoutBody.Length;
			for (int i = 0; i < this.Content.Length; i++)
			{
				response[contentBufferSatrtIndex + i] = this.Content[i];
			}

			return response;
		}

		public override string ToString()
		{
			StringBuilder responseText = new StringBuilder();
			responseText.Append($"{GlobalConstants.HttpOneProtocolFragment} {(int)this.StatusCode} {this.StatusCode.GetStatusMessage()}")
				.Append(GlobalConstants.HttpNewLine)
				.Append(Headers)
				.Append(GlobalConstants.HttpNewLine)
				.Append(Cookies);

			responseText.Append(GlobalConstants.HttpNewLine);
			return responseText.ToString();
		}
	}
}
