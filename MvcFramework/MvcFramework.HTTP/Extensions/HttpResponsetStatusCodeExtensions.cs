using MvcFramework.HTTP.Enums;

namespace MvcFramework.HTTP.Extensions
{
	public static class HttpResponseStatusCodeExtensions
	{
		public static string GetStatusMessage(this HttpResponseStatusCode statusCode)
		{
			switch (statusCode)
			{
				case HttpResponseStatusCode.BadRequest: return "Bad Request";
				case HttpResponseStatusCode.Created: return "Created";
				case HttpResponseStatusCode.Forbidden: return "Forbidden";
				case HttpResponseStatusCode.Found: return "Found";
				case HttpResponseStatusCode.Ok: return "Ok";
				case HttpResponseStatusCode.Unauthorized: return "Unauthorized";
				case HttpResponseStatusCode.SeeOther: return "See other";
				case HttpResponseStatusCode.NotFound: return "Not Found";
				case HttpResponseStatusCode.InernalServerError: return "Inrenal Server Error";
				default: return "Not suported status code";
			}
		}
	}
}
