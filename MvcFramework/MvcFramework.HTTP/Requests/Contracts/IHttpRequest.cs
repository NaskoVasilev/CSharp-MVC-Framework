using MvcFramework.HTTP.Cookies.Contracts;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers.Contracts;
using MvcFramework.HTTP.Sessions.Contracts;
using System.Collections.Generic;

namespace MvcFramework.HTTP.Requests.Contracts
{
	public interface IHttpRequest
	{
		string Path { get; }

		string Url { get; }

		Dictionary<string, ISet<string>> FormData { get; }

		Dictionary<string, ISet<string>> QueryData { get; }

		IHttpHeaderCollection Headers { get; }

		IHttpCookieCollection Cookies { get; }

		HttpRequestMethod RequestMethod { get; }

		IHttpSession Session { get; set; }
	}
}
