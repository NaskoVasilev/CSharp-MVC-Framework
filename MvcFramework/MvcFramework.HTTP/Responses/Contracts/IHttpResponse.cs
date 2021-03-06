﻿using MvcFramework.HTTP.Cookies;
using MvcFramework.HTTP.Cookies.Contracts;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Headers.Contracts;

namespace MvcFramework.HTTP.Responses.Contracts
{
	public interface IHttpResponse
	{
		HttpResponseStatusCode StatusCode { get; set; }

		IHttpHeaderCollection Headers { get; }

		IHttpCookieCollection Cookies { get; }

		byte[] Content { get; set; }

		void AddHeader(string key, string value);

		void AddCookie(HttpCookie cookie);

		byte[] GetBytes();
	}
}
