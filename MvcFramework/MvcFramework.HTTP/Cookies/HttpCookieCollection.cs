using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Cookies.Contracts;
using MvcFramework.HTTP.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvcFramework.HTTP.Cookies
{
	public class HttpCookieCollection : IHttpCookieCollection
	{
		private readonly Dictionary<string, HttpCookie> httpCookies;

		public HttpCookieCollection()
		{
			this.httpCookies = new Dictionary<string, HttpCookie>();
		}

		public void AddCookie(HttpCookie httpCookie)
		{
			CoreValidator.ThrowIfNull(httpCookie, nameof(httpCookie));

			if (this.httpCookies.ContainsKey(httpCookie.Key))
			{
				throw new ArgumentException(string.Format(GlobalConstants.DuplicateCookieKey, httpCookie.Key));
			}

			this.httpCookies.Add(httpCookie.Key, httpCookie);
		}

		public bool ContainsCookie(string key)
		{
			CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

			return this.httpCookies.ContainsKey(key);
		}

		public HttpCookie GetCookie(string key)
		{
			CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

			if (!this.httpCookies.ContainsKey(key))
			{
				throw new ArgumentException(string.Format(GlobalConstants.InvalidCookieKey, key));
			}

			return this.httpCookies[key];
		}

		public bool HasCookies()
		{
			return this.httpCookies.Count > 0;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var cookie in httpCookies.Values)
			{
				sb.Append($"{HttpHeader.SetCookie}: {cookie}{GlobalConstants.HttpNewLine}");
			}

			return sb.ToString();
		}
	}
}
