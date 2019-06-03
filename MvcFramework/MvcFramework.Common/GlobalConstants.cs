namespace MvcFramework.Common
{
	public static class GlobalConstants
	{
		public const string HttpOneProtocolFragment = "HTTP/1.1";

		public const string HostHeaderKey = "Host";

		public const string HttpNewLine = "\r\n";

		public const string QueryStringPattern = @"^[^=?&]+=[^=?&]+(&[^=?&]+=[^=?&]+)*$";

		public const string QueryStringExceptionMessage = "The query string is not valid.";

		public const string RouteNotFound = "Route with method: {0} and path: {1} not found.";

		public const string InvalidCookieKey = "There is no cookie with key: {0}.";

		public const string DuplicateCookieKey = "There is already cookie with key: {0} in the cookie collection.";

		public const string ParameterDoesNotExists = "There is no parameter with name: {0} in the session";

		public const string StaticFolderName = "wwwroot";

		public const string NotFoundDefaultMessage = "The resources was not found!";

		public const string RedirectPath = "/Users/Login";

		public const string LayoutName = "_Layout.html";
	}
}
