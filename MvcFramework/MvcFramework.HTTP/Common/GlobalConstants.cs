namespace MvcFramework.HTTP.Common
{
	public static class GlobalConstants
	{
		public const string HttpOneProtocolFragment = "HTTP/1.1";

		public const string HostHeaderKey = "Host";

		public const string HttpNewLine = "\r\n";

		public const string QueryStringPattern = @"^[^=?&]+=[^=?&]+(&[^=?&]+=[^=?&]+)*$";

		public const string QueryStringExceptionMessage = "The query string is not valid.";

		public const string ContentTypeHeaderKey = "Content-Type";

		public const string LocationHeaderKey = "Content-Type";

		public const string ContentTypeHeaderTextValue = "text/plain; charset=utf-8";

		public const string ContentTypeHeaderHtmlValue = "text/html; charset=utf-8";

		public const string RouteNotFound = "Route with method: {0} and path: {1} not found.";

		public const string InvalidCookieKey = "There is no cookie with key: {0}.";

		public const string DuplicateCookieKey = "There is already cookie with key: {0} in the cookie collection.";

		public const string SetCookieKey = "Set-Cookie";

		public const string CookieHeaderKey = "Cookie";
	}
}
