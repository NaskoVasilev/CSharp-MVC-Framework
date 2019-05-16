using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers;
using MvcFramework.HTTP.Headers.Contracts;

namespace MvcFramework.HTTP.Responses.Contracts
{
	public interface IHttpResponse
	{
		HttpResponseStatusCode StatusCode { get; set; }

		IHttpHeaderCollection Headers { get; }

		byte[] Content { get; set; }

		void AddHeader(HttpHeader header);

		byte[] GetBytes();
	}
}
