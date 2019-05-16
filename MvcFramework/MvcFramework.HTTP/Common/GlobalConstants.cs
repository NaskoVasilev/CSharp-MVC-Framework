namespace MvcFramework.HTTP.Common
{
	public static class GlobalConstants
	{
		public const string HttpOneProtocolFragment = "HTTP/1.1";

		public const string HostHeaderKey = "Host";

		public const string HttpNewLine = "\r\n";

		public const string QueryStringPattern = @"^[^=?&]+=[^=?&]+(&[^=?&]+=[^=?&]+)*$";

		public const string QueryStringExceptionMessage = "The query string is not valid.";
	}
}
